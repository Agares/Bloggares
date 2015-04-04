var PostService = require('../Services/PostService.js');
var handlebars = require('handlebars');
var fs = require('fs');
var marked = require('marked');

class PostController {
	index(authenticatedUser) {
		var postService = new PostService();
		postService.all(authenticatedUser).then(function (posts) {
			document.querySelector('.container').innerHTML = '';

			for(let post of posts)
			{
				let postTemplate = handlebars.compile(fs.readFileSync('assets/templates/post.hjs', 'utf8'));
				let postHtml = postTemplate({
					Content: marked(post.Content),
					Title: post.Title
				});

				document.querySelector('.container').innerHTML += postHtml;
			}
		}, function (error) {
			alert('Unknown error while loading posts');
		});
	}
}

module.exports = PostController;