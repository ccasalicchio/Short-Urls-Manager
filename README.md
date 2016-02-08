# Short Urls (Puul.ga)
This project is open source and can be used freely anywhere. The only request is to keep references to the author.

The website is designed as a free service that shortens urls much like bit.ly and others. 

It redirects simple urls to full urls.

###Make sure to change the web.config to include your settings:

*AppSettings*

    Key=="webpages:Version" value="3.0.0.0" ==> DO NOT CHANGE
    
    Key=="webpages:Enabled" value="false" ==> DO NOT CHANGE
    
    Key=="ClientValidationEnabled" value="true" ==> DO NOT CHANGE
    
    Key=="UnobtrusiveJavaScriptEnabled" value="true" ==> DO NOT CHANGE
    
    Key=="LenghtOfRandomString" value="8" ==> LENGHT OF SHORT URL
    
    Key=="WebsiteName" value="Puulga" ==> TITLE OF THE WEBSITE
    
    Key=="WebsiteUrl" value="http://puul.ga/" ==> URL OF THE SITE (USED FOR STATIC ITEMS, SUCH AS EMAILS)
    
    Key=="Website" value="true" ==> USED IN CASE OF SHARED HOSTING AND XML FILES (BUGGY) need to change it 
    to HostingEnvironment.MapPath
    
#Email provider section
    
    
    Key=="siteadmin" value="email@puul.ga" ==> EMAIL ADDRESS TO SEND EXCEPTIONS EMAILS
    
    Key=="email-subject" value="Email from Puul.ga" ==> EMAIL SUBJECT
    
    Key=="email-from" value="email@puul.ga" ==> EMAIL FROM
    
    Key=="email-from-name" value="Puul.ga" ==> EMAIL FROM NAME
    
    Key=="email-require-auth" value="false" ==> IN CASE OF SMTP SERVERS THAT REQUIRE AUTHENTICATION
    
    Key=="email-username" value="puul.ga" ==> USERNAME FOR SMTP SERVER
    
    Key=="email-password" value="password" ==> PASSWORD FOR SMTP SERVER
    
    Key=="email-port" value="3535" ==> SMTP OUT PORT
    
    Key=="useSSL" value="false" ==> WHETHER TO USE SSL WITH SMTP
    
    Key=="email-server" value="localhost" ==> URL OF THE SMTP SERVER
    
    
#PATHS ARE RELATIVE SINCE THE SERVER WILL ATTEMPT TO MAP THE LOCATION
    
    
    Key=="email-html-share" value="~/Email Templates/share.html" ==> HTML FOR THE SHARE ICONS
    
    Key=="email-html-default" value="~/Email Templates/puulga.html" ==> HTML FOR THE DEFAULT EMAIL
    
    Key=="email-html-forgotten" value="~/Email Templates/puulga.html" ==> HTML FOR THE FORGOTTEN PASSWORD EMAIL
    
    Key=="email-html-confirmation" value="~/Email Templates/puulga.html" ==> HTML FOR THE CONFIRMATION EMAIL
    
    Key=="email-html-welcome" value="~/Email Templates/puulga.html" ==> HTML FOR THE WELCOME EMAI
    
#Database options. possible values: Xml, Sql
    
    
    Key=="DbProvider" value="Sql" ==> IN CASE OF SQL AND XML FOR XML FILE
    
    Key=="ForwardsFile" value="~/App_Data/forwards.xml" ==> IF USING XML AS DATA STORE, SET THE FILE LOCATION



[email me] (mailto:carlos.casalicchio@gmail.com) me if you have questions
