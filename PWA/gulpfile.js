var gulp = require("gulp"),
    fs = require("fs"),
    less = require("gulp-less"),
    sass = require("gulp-sass");

// other content removed

gulp.task("sass", function () {
    return gulp.src('/wwwroot/cscc/custom.scss')
        .pipe(sass())
        .pipe(gulp.dest('/wwwroot/styles/'));
});