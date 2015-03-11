import superagent from 'superagent';
require('babel/polyfill');

export default class PostService
{
	all()
	{
		var promise = new Promise(function(resolve, reject) {
			superagent.get('/api/posts').end(function(error, resource) {
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