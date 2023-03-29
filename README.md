# Kfone Admin Application using .NET

Kfone, a telecommunication service that offers telecom and digital products to customers. 
The admin portal has been built for Kfone staff to add devices and services for sale and make seasonal updates to available items. 
The staff is divided into three categories: 
* Admin
  * Admin can add and update devices, create and change customer tiers. 
* Sales Representative
  * Sales Representative can add promo codes, assign them to items, and decide if an item is ready for sale. 
* Marketing Lead
  * Marketing Lead can view the dashboard to see user activities. 

### Access Control

* Access to the application is restricted to staff members only, achieved through an adaptive script and group-based access control policy. 

Adaptive script will allow access for any user who belongs to `staff` group. Every Admin, Sales Representative, Marketing Lead is belongs to the `staff` group.
```
var groupsToAllowAccess = ['staff'];

var errorPage = '';

var errorPageParameters = {
    'status': 'Unauthorized',
    'statusMsg': 'You are not authorized to login to this application.'
};

var onLoginRequest = function(context) {
    executeStep(1, {
        onSuccess: function (context) {
            // Extracting authenticated subject from the first step.
            var user = context.currentKnownSubject;
            // Checking if the user is assigned to one of the given groups.
            var isMember = isMemberOfAnyOfGroups(user, groupsToAllowAccess);
            if (!isMember) {
                sendError(errorPage, errorPageParameters);
            }
            executeStep(2);
        }
    });
};
```
![image](https://user-images.githubusercontent.com/43197743/228460182-1e711739-94e2-4443-845d-260e521f46e3.png)

* Application views are restricted based on staff category using group-based access control. 

### MFA

MFA has been implemented for staff members, using multi-options for basic authentication and magic link for step one, and TOTP for step two. 
Backup codes can only be used by the administrator. 

