require('babel/polyfill');

var PostService = require('./Services/PostService.js');
var AuthenticationService = require('./Services/AuthenticationService.js');

var marked = require('marked');
var handlebars = require('handlebars');
var fs = require('fs');

let loginTemplate = handlebars.compile(fs.readFileSync('assets/templates/login.hjs', 'utf8'));
let loginHtml = loginTemplate();

document.addEventListener('DOMContentLoaded', function () {
	document.querySelector('body').innerHTML += loginHtml;

	document.querySelector('#form-login').addEventListener('submit', function (event) {
		event.preventDefault();

		let username = document.querySelector('#form-login-username').value;
		let password = document.querySelector('#form-login-password').value;

		let authenticationService = new AuthenticationService();
		var authorizedUser = authenticationService
			.authenticate(username, password)
			.then(function (authorizedUser) {
				console.log(authorizedUser);
			});
	});
});

/*
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
});*/