var superagent = require('superagent');

class PostService
{
	all(user) {
		var promise = new Promise((resolve, reject) => {
			superagent
				.get('/api/posts')
				.set('X-Bloggares-Token', user.Token)
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

module.exports = PostService;