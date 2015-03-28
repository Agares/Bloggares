import superagent from 'superagent';
require('babel/polyfill');

export default class PostService
{
	all()
	{
		var promise = new Promise((resolve, reject) => {
			superagent
				.get('/api/posts')
				.query({token: '5a1f0776-525d-45a4-be08-86f70a8670ed'})
				.end(function(error, resource) {
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