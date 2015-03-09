module.exports = function (grunt) {
    grunt.initConfig({
        browserify: {
            dist: {
                options: {
                    transform: ['babelify']
                },
                files: {
                    'wwwroot/assets/js/application.js': 'assets/js/application.js'
                }
            }
        },
        sass: {
            dist: {
                files: {
                    'wwwroot/assets/css/site.css': 'assets/sass/site.scss'
                }
            }
        },
        copy: {
        	dist: {
        		files: {
        			'wwwroot/assets/css/lib/normalize.css': 'node_modules/normalize.css/normalize.css'
        		}
        	}
        },
        watch: {
        	js: {
        		files: ['assets/js/**'],
        		tasks: ['browserify']
        	},
        	css: {
        		files: ['assets/sass/**'],
				tasks: ['copy', 'sass']
        	}
        }
    });

    grunt.loadNpmTasks('grunt-browserify');
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');

    grunt.registerTask('default', ['browserify', 'sass']);
};