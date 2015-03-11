import PostService from './Services/PostService.js';
import marked from 'marked';
import handlebars from 'handlebars';
var fs = require('fs'); // todo why import doesn't work here?

var service = new PostService();
service.all().then(function(posts) {
	for(let post of posts)
	{
		let postTemplate = handlebars.compile(fs.readFileSync('assets/templates/post.hjs', 'utf8'));
		let postHtml = postTemplate({
			Content: marked(post.Content),
			Title: post.Title
		});

		document.querySelector('body').innerHTML += postHtml;
	}
});