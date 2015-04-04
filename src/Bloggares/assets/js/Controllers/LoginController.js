var AuthenticationService = require('../Services/AuthenticationService.js');
var handlebars = require('handlebars');
var fs = require('fs');

class LoginController {
	constructor(authenticationService)
	{
		this.authenticationService = authenticationService;
	}

	showLoginForm() {
		let loginTemplate = handlebars.compile(fs.readFileSync('assets/templates/login.hjs', 'utf8'));
		let loginHtml = loginTemplate();

		document.querySelector('.container').innerHTML = loginHtml;

		return new Promise((resolve, reject) => {
			document
				.getElementById('form-login')
				.addEventListener('submit', (event) => {
					event.preventDefault();

					let username = document.getElementById('form-login-username').value;
					let password = document.getElementById('form-login-password').value;

					this.authenticationService
						.authenticate(username, password)
						.then(function (authorizedUser) {
							resolve(authorizedUser);
						})
						.catch(reject);
			});
		});
	}
}

module.exports = LoginController;