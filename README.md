# PetsMongo
in order for the controllers to work a bearer token is needed

start the api
a swagger window apper
go to UsersController
go to authenticate and insert {"userName":"user1","password":"password1"} as cradentials
now you got a token and you can go to the main controller - PetsController
you can insert in PostMan the token you got and yoy are good to go

example json to insert to db
{
  "name": "Yami",
  "createdAt": "2023-01-24",
  "deletedAt": null,
  "type": "Dog",
  "age": 3
}

didnt get to insert caching
