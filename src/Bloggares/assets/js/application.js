require('babel/polyfill');

var PostService = require('./Services/PostService.js');
var AuthenticationService = require('./Services/AuthenticationService.js');
var HomeController = require('./Controllers/HomeController.js');

var marked = require('marked');

var controller = new HomeController(new AuthenticationService);

document.addEventListener('DOMContentLoaded', function () {
	controller.index();
});