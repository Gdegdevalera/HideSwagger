# HideSwagger plugin example

Swagger should be hidden for the production environment.
It is possible to do this by using conditions in code or by API routes filtering on the reverse proxy level.

But let me introduce another solution for this issue. We will use your system authorization. 

This example is for OAuth2 with login\password authorization.

Unauthorized visitor will see nothing in our SwaggerUI page:

![](https://i.imgur.com/bvZ1kUv.png)

But routes will appear after authorization:

![](https://i.imgur.com/b9bL4jy.png)

For this example
Login: user
Password: pass

For this purposes we need to do the next staps:
1. Add document filter to hide Swagger specs in swagger.json file
2. Add swagger ui plugin to reload spec file with authorization token
3. Be sure that every request for our API follwed by authorization

This commit is better than 1000 words: https://github.com/Gdegdevalera/HideSwagger/commit/3ed1978e6cb34abd53eeaaa94fd6604f14f05e03
