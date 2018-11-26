# Sasken HOL - Azure + DevOps
 
## Pre-requisites:

* Clone the repo: https://monojit18@github.com/monojit18/Sasken_Workshop.git
* You will get 5 projects - EasyPostsApp (.NET web api), EasyMobileApp (.NET web api), HelloWebSite (.NET MVC site),
TestAzureFunction (Azure Function), TestServerless (Azure Function)

*You can ignore follwing 4 steps you already have the Azure portal setup done*

* Create a resource group (e.g. sknworkshopgroup) in the Azure portal
* Create an App service plan in the process (e.g. sknserviceplan)
* Create a Storage in the process (e.g. sknstorage)
* Select a region for the resource group (e.g. EastUS)

* Make sure you have VS for Windows installed
* VS should have Azure Function & WebJobs tools installed
* For MAc users, VSCode is a good option, as you would get publish option after connecitng to
Azure subscription all from within the VSCode IDE

*Azure DevOps steps - mandatory*

* Go to https://dev.azure.com
* Anyone of the team member create an account for your organization (e.g. sknDevOps)
* Add few Team members to it
* Others check the login and access
* Create a repository project for each of the above projects - You can name them as you would like!
* Create Azure Repos link for all of these
* Clone each empty project on your local machine

### Exercise 1:

1.	Go to Azure portal
2.	Create a new .NET based web API resource (e.g. QuickPostsApp)  – under the resource group created above (e.g. sknworkshopgroup)
3.	Note the Url
4.	Create a CosmosDB resource with Mongo API (e.g. skncosmosmongodb)
5.	Note down the URL and Access Key from the QuickStart menu option
6.	Now create an empty Web API project in VS – named – QuickPostsApp – may be
7.	Implement GET and PUT methods which in-turn would call appropriate mongo APIs
8.	Publish the project from VS to Azure
9.	If you have mongo installed locally – please test it once before publishing
*Refer to EasyPostsApp in repo whenever you feel the need*

10. Publish the project from Visual Studio or VScode - your preferred IDE
11. Test the implemenattion in the cloud once.

### Exercise 2:

1.	Go to Azure portal
2.	Create a new .NET based web API resource (e.g. QuickMobileApp)  – under the resource group created above (e.g. sknworkshopgroup)
3.	Note the Url
4.	Open Storage emulator either on you local machine or in Azure portal for sknstorage that you had created earlier
5.	Add a blob under Blob container (e.g. QuickBlob)
6.	Add a table under Table container (e.g. QuickTable)
7.	Note down the Primary Connection string for the storage account
8.	Implement GET and PUT methods which in-turn would call appropriate Storage APIs
*Refer to EasyMobileApp in repo whenever you feel the need*

10. Publish the project from Visual Studio or VScode - your preferred IDE
11. Add necessary environment variables from local web.config to the cloud app in portal - under Application settings
12. Test the implemenattion in the cloud once.

### Exercise 3:


1.	Go to Azure portal
2.	Create a Function instance
3.	Install windows Storage emulator on your desktop
4.	Connect to the nsistorage you had created earlier
5.	Create a Blob Container and Queue container
6.	Open TestAzureFunction project in cloned repo
7.	Execute ProcessBlob by triggering Blob update – using Storage emulator
8.	Try enabling the resiliency function and see how it affects the function flow through Live Metric in portal

### Exercise 4:

*Following steps will be done in details duriong the workshop and discussed in-depth both for Standard deployment

1. Go back to Azure DevOps
2. Define Build and Release pipeline - will be shown in the workshop in details
3. Come back to VSCode
4. Check in your code to the Git repo for each of the projects
5. Go back to DevOps portal and see how Build and Release happens, sequentially


Reference:

1. Azure AD - https://docs.microsoft.com/en-us/azure/active-directory/

2. Azure API Management - https://docs.microsoft.com/en-us/azure/api-management/

3 Serverless -    https://docs.microsoft.com/en-us/azure/azure-functions/
                https://docs.microsoft.com/en-us/azure/logic-apps/
        
4. Azure Cognitive Services - https://docs.microsoft.com/en-us/azure/cognitive-services/
                
5. DevOps - https://docs.microsoft.com/en-us/azure/devops/index?view=vsts


