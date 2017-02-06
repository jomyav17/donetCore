/// <binding AfterBuild='minify' />
var gulp = require('gulp');
var uglify = require('gulp-uglify');

gulp.task('minify', function () {
    return gulp.src("wwwroot/js/*.js")
            .pipe(uglify())
            .pipe(gulp.dest("wwwroot/lib/_app"))
            .on('error', function (err) {
                console.error('Error in compress task', err.toString());
            });
});

//var gutil = require('gulp-util');

//$.uglify().on('error', function (err) {
//    gutil.log(gutil.colors.red('[Error]'), err.toString());
//    this.emit('end');
//})