//This code was copied from the Google Calendar API .Net Quickstart tutorial, here: 
//https://developers.google.com/google-apps/calendar/quickstart/dotnet

//This version of the project will be for adding an event to Google Calendar.
//This references the "Create Events" tab:
//https://developers.google.com/google-apps/calendar/create-events
//along with this page:
//https://developers.google.com/google-apps/calendar/v3/reference/events

//For the actual code, I referenced Linda Lawton (of Google) who directed me to her GitHub 
//document that covers this:
//https://github.com/LindaLawton/Google-Dotnet-Samples/blob/master/Google-Calendar/Google-Calendar-Api-dotnet/Google-Clndr-Api-dotnet/Google-Clndr-Api-dotnet/DaimtoEventHelper.cs

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleCalendarApplication1
{
    class Program
    {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath + "\n");

            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            //These are variables for storing a simple event. 
            string yesOrNo = "";
            string eventName = "";
            string eventDescription = "";
            string startDate = "";
            string endDate = "";

            int eventCount = 0;

            bool isTrue = true;



            while (isTrue == true)
            {
                string welcomeMessage = "";


                if (eventCount > 0)
                {
                    welcomeMessage = "Would you like to add another event to your Google Calendar (Y/N)?\n";
                }
                else if (eventCount == 0)
                {
                    welcomeMessage = "Would you like to add an event to your Google Calendar (Y/N)?\n";
                }

                Console.WriteLine(welcomeMessage);
                yesOrNo = Console.ReadLine();

                if (yesOrNo == "Y" || yesOrNo == "y")
                {
                    bool noName = true;

                    while (noName == true)
                    {
                        Console.WriteLine("\nWhat will the name of your event be?\n");
                        eventName = Console.ReadLine();

                        if (eventName == "")
                        {
                            Console.WriteLine("\nYou must enter a name for your event.\n");
                        }
                        else
                        {
                            noName = false;

                            bool noDescription = true;

                            while (noDescription == true)
                            {
                                Console.WriteLine("\nWhat will the description of your event be?\n");
                                eventDescription = Console.ReadLine();

                                if (eventDescription == "")
                                {
                                    Console.WriteLine("\nYou must enter a description for your event.\n");
                                }
                                else
                                {
                                    noDescription = false;

                                    bool noStart = true;

                                    while (noStart == true)
                                    {
                                        Console.WriteLine("\nWhat is the start date and time of your event?\n");
                                        startDate = Console.ReadLine();

                                        if (startDate == "")
                                        {
                                            Console.WriteLine("\nYou must enter a start date and time.\n");
                                        }
                                        else
                                        {
                                            noStart = false;

                                            bool noEnd = true;

                                            while (noEnd == true)
                                            {
                                                Console.WriteLine("\nWhat is the end date and time of your event?\n");
                                                endDate = Console.ReadLine();

                                                if (endDate == "")
                                                {
                                                    Console.WriteLine("\nYou must enter an end date for your event.\n");
                                                }
                                                else
                                                {
                                                    string readyToAdd = "";

                                                    noEnd = false;
                                                    Console.WriteLine("\nAre you ready to add your event (Y/N)?\n");
                                                    readyToAdd = Console.ReadLine();

                                                    if (readyToAdd == "Y" || readyToAdd == "y")
                                                    {
                                                        eventCount++;
                                                        Console.WriteLine("\nYou have added the following event to your Google Calendar: \n Title: " + eventName + "\n Description: " + eventDescription +
                                                            "\n Start Date: " + startDate + "\n End Date:" + endDate + "\n");
                                                    }
                                                }

                                            }

                                        }
                                    }

                                }
                            }
                        }
                        }
                }
                else if (yesOrNo == "N" || yesOrNo == "n")
                {
                    Console.WriteLine("Thank you.  Your program is exiting.");
                    System.Environment.Exit(1);
                    isTrue = false;
                }
                else
                {
                    Console.WriteLine("You must give a valid input of  Y, y, N, or n.");
                }

                // Insert an event.
                // This is based on the tutorial here:
                //https://developers.google.com/google-apps/calendar/create-events
                //Event newEvent = new Event()
                //    .setSummary("Meeting with Prof. Oberst")
                //    .setLocation("11333 Big Bend Rd, St. Louis, MO 63122")
                //    .setDescription("This meeting will discuss how far I've gotten with Google Calendar API.");
            }
        }
    }
}

//This was successful.


///////////////////////////////
//   REFERENCE EVENTS        //
///////////////////////////////

//Visit: https://developers.google.com/google-apps/calendar/v3/reference/events

//{
//  "kind": "calendar#event",
//  "etag": etag,
//  "id": string,
//  "status": string,
//  "htmlLink": string,
//  "created": datetime,
//  "updated": datetime,
//  "summary": string,
//  "description": string,
//  "location": string,
//  "colorId": string,
//  "creator": {
//    "id": string,
//    "email": string,
//    "displayName": string,
//    "self": boolean
//  },
//  "organizer": {
//    "id": string,
//    "email": string,
//    "displayName": string,
//    "self": boolean
//  },
//  "start": {
//    "date": date,
//    "dateTime": datetime,
//    "timeZone": string
//  },
//  "end": {
//    "date": date,
//    "dateTime": datetime,
//    "timeZone": string
//  },
//  "endTimeUnspecified": boolean,
//  "recurrence": [
//    string
//  ],
//  "recurringEventId": string,
//  "originalStartTime": {
//    "date": date,
//    "dateTime": datetime,
//    "timeZone": string
//  },
//  "transparency": string,
//  "visibility": string,
//  "iCalUID": string,
//  "sequence": integer,
//  "attendees": [
//    {
//      "id": string,
//      "email": string,
//      "displayName": string,
//      "organizer": boolean,
//      "self": boolean,
//      "resource": boolean,
//      "optional": boolean,
//      "responseStatus": string,
//      "comment": string,
//      "additionalGuests": integer
//    }
//  ],
//  "attendeesOmitted": boolean,
//  "extendedProperties": {
//    "private": {
//      (key): string
//    },
//    "shared": {
//      (key): string
//    }
//  },
//  "hangoutLink": string,
//  "gadget": {
//    "type": string,
//    "title": string,
//    "link": string,
//    "iconLink": string,
//    "width": integer,
//    "height": integer,
//    "display": string,
//    "preferences": {
//      (key): string
//    }
//  },
//  "anyoneCanAddSelf": boolean,
//  "guestsCanInviteOthers": boolean,
//  "guestsCanModify": boolean,
//  "guestsCanSeeOtherGuests": boolean,
//  "privateCopy": boolean,
//  "locked": boolean,
//  "reminders": {
//    "useDefault": boolean,
//    "overrides": [
//      {
//        "method": string,
//        "minutes": integer
//      }
//    ]
//  },
//  "source": {
//    "url": string,
//    "title": string
//  },
//  "attachments": [
//    {
//      "fileUrl": string,
//      "title": string,
//      "mimeType": string,
//      "iconLink": string,
//      "fileId": string
//    }
//  ]
//}



///////////////////////////////
//      JAVA REFERENCE       //
///////////////////////////////

// Refer to the Java quickstart on how to setup the environment:
// https://developers.google.com/google-apps/calendar/quickstart/java
// Change the scope to CalendarScopes.CALENDAR and delete any stored
// credentials.

//Event event = new Event()
//    .setSummary("Google I/O 2015")
//    .setLocation("800 Howard St., San Francisco, CA 94103")
//    .setDescription("A chance to hear more about Google's developer products.");

//DateTime startDateTime = new DateTime("2015-05-28T09:00:00-07:00");
//EventDateTime start = new EventDateTime()
//    .setDateTime(startDateTime)
//    .setTimeZone("America/Los_Angeles");
//event.setStart(start);

//DateTime endDateTime = new DateTime("2015-05-28T17:00:00-07:00");
//EventDateTime end = new EventDateTime()
//    .setDateTime(endDateTime)
//    .setTimeZone("America/Los_Angeles");
//event.setEnd(end);

//String []
//recurrence = new String[] {"RRULE:FREQ=DAILY;COUNT=2"};
//event.setRecurrence(Arrays.asList(recurrence));

//EventAttendee []
//attendees = new EventAttendee[] {
//    new EventAttendee().setEmail("lpage@example.com"),
//    new EventAttendee().setEmail("sbrin@example.com"),
//};
//event.setAttendees(Arrays.asList(attendees));

//EventReminder []
//reminderOverrides = new EventReminder[] {
//    new EventReminder().setMethod("email").setMinutes(24 * 60),
//    new EventReminder().setMethod("popup").setMinutes(10),
//};
//Event.Reminders reminders = new Event.Reminders()
//    .setUseDefault(false)
//    .setOverrides(Arrays.asList(reminderOverrides));
//event.setReminders(reminders);

//String calendarId = "primary";
//event = service.events().insert(calendarId, event).execute();
//System.out.printf("Event created: %s\n", event.getHtmlLink());


///////////////////////////////////
//    LINDA LAWTON'S CODE FROM   //
//      Google-Dotnet-Samples    //
//     Google-Clndr-Api-dotnet   //
///////////////////////////////////

//....
//#region Insert

///// <summary>
///// Adds an entry to the user's calendar list.  
///// Documentation:https://developers.google.com/google-apps/calendar/v3/reference/calendarList/insert
///// </summary>
///// <param name="service">Valid Autenticated Calendar service</param>
///// <param name="id">Calendar identifier.</param>
///// <param name="body">an event</param>
///// <returns>event resorce: https://developers.google.com/google-apps/calendar/v3/reference/events#resource </returns>
//public static Event insert(CalendarService service, string id, Event body)
//{
//    try
//    {
//        return service.Events.Insert(body, id).Execute();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//        return null;
//    }
//}


//#endregion
//....