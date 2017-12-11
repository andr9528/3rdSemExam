This File included description of different method that reside inside the Service and how to call them succesfully.

The Delete method takes in 2 parameters, both being a 'int'
	First is called 'type' and is very picky about what it accepts
		To delete from Worker pass a '1' to 'type'
        To delete from Work Entries pass a '2' to 'type'
        To delete from Tasks pass a '3' to 'type'
        To delete from Projects pass a '4' to 'type'
	Secound is called 'id' and accepts any int, but the method will throw a exception,
	if no object with the specified id of that type can be found.