var superagent = require('superagent');

class AuthenticationService
{
	authenticate(username, password) {
		var promise = new Promise((resolve, reject) => {
			superagent
				.post('/api/authorization') // todo controller and route should contain "authentication" not "authorization"
				.type('form')
				.send({ username: username, password: password })
				.end(function (error, resource) {
					if (resource.ok) {
						resolve(JSON.parse(resource.text));
					} else {
						reject(error);
					}
				});
		});

		return promise;
	}
}

module.exports = AuthenticationService;