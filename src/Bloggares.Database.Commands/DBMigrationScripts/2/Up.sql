CREATE TABLE tokens (
	id SERIAL PRIMARY KEY, 
	username VARCHAR(255), 
	token UUID, 
	validUntil TIMESTAMP WITH TIME ZONE
);

-- todo foreign key on username