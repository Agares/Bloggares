var LoginController = require('./LoginController');
var PostController = require('./PostController.js');

class HomeController {
	constructor(authenticationService)
	{
		this.authenticationService = authenticationService;
	}

	index() {
		let loginController = new LoginController(this.authenticationService);
		let postController = new PostController();

		loginController
			.showLoginForm()
			.then(function (user) {
				postController.index(user);
			})
			.catch(function () {
				alert('Invalid username or password.');
			});
	}
}

module.exports = HomeController;