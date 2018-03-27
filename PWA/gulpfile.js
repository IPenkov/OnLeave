var gulp = require("gulp"),
    fs = require("fs"),
    less = require("gulp-less"),
    sass = require("gulp-sass");

// other content removed
gulp.task("sass", function () {
    return gulp.src('wwwroot/scss/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('wwwroot/styles'));
});