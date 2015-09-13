using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.DevView.DummyServer
{
    class GenericProvider<T> where T : IUpdateable
    {
        internal delegate T Factory(int id, Random random);

        private List<T> _objects = new List<T>();
        private HashSet<T> _updatedObjects = new HashSet<T>();
        protected Random _random = new Random();
        private int _lastObjectId = 0;
        private DateTime _lastUpdate = DateTime.MinValue;
        private static readonly List<T> Empty = new List<T>();
        protected NLog.ILogger _log;
        private Factory _factory;

        public GenericProvider(Factory factory)
        {
            _factory = factory;
        }

        protected void CreateObject()
        {
            var obj = _factory(++_lastObjectId, _random);
            _log.Info("Happy instantiation day, {0}!", obj);
            _objects.Add(obj);
            _updatedObjects.Add(obj);
        }

        protected T GetObject(int id)
        {
            return _objects.SingleOrDefault(o => o.Id == id);
        }

        protected ICollection<T> GetObjects()
        {
            return _objects;
        }

        protected ICollection<T> GetUpdatedObjects()
        {
            if (_updatedObjects.Count < 50 && (DateTime.Now - _lastUpdate) < TimeSpan.FromSeconds(10))
            {
                _log.Debug("Suppress update; found {0} entities and the time is {1}", _updatedObjects.Count, DateTime.Now - _lastUpdate);
                return Empty;
            }

            var result = new List<T>(_updatedObjects);
            _updatedObjects.Clear();
            _log.Debug("Send update of {0} objects", result.Count);
            _lastUpdate = DateTime.Now;
            return result;
        }

        protected void AddToUpdatedList(T updatee)
        {
            _updatedObjects.Add(updatee);
        }

        public void Update()
        {
            if (_random.NextDouble() < 0.05)
                CreateObject();

            for (int i = _objects.Count - 1; i >= 0; --i)
            {
                var obj = _objects[i];
                switch (obj.Update(_random))
                {
                    case UpdateOutcome.None:
                        continue;
                    case UpdateOutcome.Removed:
                        _log.Info("{0} expired.", obj);
                        _objects.RemoveAt(i);
                        ++i;
                        goto case UpdateOutcome.Changed;
                    case UpdateOutcome.Changed:
                        _updatedObjects.Add(obj);
                        break;
                }
            }
        }
    }
}
