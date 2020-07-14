

# azure-ad-graph-demo

A simple demo that leverages Azure CLI access token and invokes graph api to retrived User details or the manager information of any user.


# Dependencies

This application uses raw ```HttpClient``` hence there no dependecies are required, except ```Newtonsoft.Json```.

# How to run it?

First of all you need to run the Azure CLI login:

```
az login
```

This would cache the **Access Token** into your file system. Which will be consumed by the application later on.

Once the login was completed you can run the application:

```
dotnet run
```

This would ask for a User Object ID. And a mode - either ```U``` or ```M```.
For user ID you can either enter the object ID (AD object ID) or an UPN (e.g. email account) - both should work.

```U``` : Is to query user details via Graph api
And 
```M``` : Is to query the manager info first then getting the details of the manager object.

### That's all