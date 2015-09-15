/// <binding ProjectOpened='watch' />
'use strict';

var gulp = require('gulp'),
    debug = require('gulp-debug'),
    tsc = require('gulp-typescript'),
    seq = require('run-sequence'),
    rimraf = require('rimraf');

var paths = {
    typescript: {
        source: 'TypeScript/**/*.ts',
        output: 'webroot/ts'
    }
};

// Cleans all previously compiled TS files
gulp.task('clean', function (cb) {
    rimraf(paths.typescript.output, cb);
});

// Compiles all TypeScript
gulp.task('compile-ts', function () {
    gulp.src(paths.typescript.source)
        //.pipe(debug({title: 'compile file: '}))
        .pipe(tsc())
        //.pipe(debug({title: 'compiled:'}))
        .pipe(gulp.dest(paths.typescript.output));
});

gulp.task('watch', ['compile-ts'], function () {
    gulp.watch(paths.typescript.source, ['compile-ts']);
});

gulp.task('default', [ 'compile-ts' ], function () {
    
});