namespace HabitRPG.Client.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public class User : Member
    {
        #region Properties

        [JsonProperty("dailys")]
        public List<Daily> Dailys { get; set; }

        [JsonProperty("habits")]
        public List<Habit> Habits { get; set; }

        [JsonProperty("rewards")]
        public List<Reward> Rewards { get; set; }

        [JsonProperty("todos")]
        public List<Todo> Todos { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("challenges")]
        public List<Guid> Challenges { get; set; }

        #endregion Properties

        public static async Task<User> GetAsync()
        {
            var response = await HttpClient.GetAsync("user");
            return GetResult<User>(response);

            /*
            
            Sample Return:

            {
	            "success": true,
	            "data": {
		            "_id": "cfd9941b-037c-45bc-9d86-320067a4de3e",
		            "_ABTests": {
			            "onboardingPushNotification": "Onboarding-Step7-Phasea-VersionC"
		            },
		            "invitesSent": 0,
		            "loginIncentives": 4,
		            "webhooks": [],
		            "pushDevices": [],
		            "extra": {
			
		            },
		            "tasksOrder": {
			            "rewards": [],
			            "todos": [],
			            "dailys": [],
			            "habits": []
		            },
		            "inbox": {
			            "optOut": false,
			            "messages": {
				
			            },
			            "blocks": [],
			            "newMessages": 0
		            },
		            "tags": [{
			            "name": "Work",
			            "id": "4f97be92-8879-4246-b4e0-f0a1c211d0c2"
		            },
		            {
			            "name": "Exercise",
			            "id": "e2e67fba-b231-4b70-a663-98365c25a4c5"
		            },
		            {
			            "name": "Health + Wellness",
			            "id": "739bb30c-f12c-41fe-a8be-0294b367ef03"
		            },
		            {
			            "name": "School",
			            "id": "671a2636-e982-4c5c-8cdd-448645278d8e"
		            },
		            {
			            "name": "Teams",
			            "id": "0c23ff1a-5672-45cd-aa1c-854b8a22991d"
		            },
		            {
			            "name": "Chores",
			            "id": "60f94188-1801-403d-9171-4558f62d2449"
		            },
		            {
			            "name": "Creativity",
			            "id": "4ffc5eba-7d43-4052-8b74-d595f67c99f0"
		            }],
		            "notifications": [{
			            "type": "CRON",
			            "data": {
				            "mp": 0,
				            "hp": 0
			            },
			            "id": "bc817158-da5b-49ae-ad63-b6d41eaeb629"
		            }],
		            "stats": {
			            "training": {
				            "con": 0,
				            "str": 0,
				            "per": 0,
				            "int": 0
			            },
			            "buffs": {
				            "seafoam": false,
				            "shinySeed": false,
				            "spookySparkles": false,
				            "snowball": false,
				            "streaks": false,
				            "stealth": 0,
				            "con": 0,
				            "per": 0,
				            "int": 0,
				            "str": 0
			            },
			            "per": 0,
			            "int": 0,
			            "con": 0,
			            "str": 0,
			            "points": 3,
			            "class": "warrior",
			            "lvl": 3,
			            "gp": 51.71066399999999,
			            "exp": 2,
			            "mp": 32,
			            "hp": 50,
			            "toNextLevel": 170,
			            "maxHealth": 50,
			            "maxMP": 32
		            },
		            "profile": {
			            "name": "dotnet_test"
		            },
		            "preferences": {
			            "language": "en",
			            "background": "violet",
			            "timezoneOffsetAtLastCron": 420,
			            "improvementCategories": [],
			            "tasks": {
				            "confirmScoreNotes": false,
				            "groupByChallenge": false
			            },
			            "suppressModals": {
				            "streak": false,
				            "raisePet": false,
				            "hatchPet": false,
				            "levelUp": false
			            },
			            "pushNotifications": {
				            "invitedQuest": true,
				            "questStarted": true,
				            "invitedGuild": true,
				            "invitedParty": true,
				            "giftedSubscription": true,
				            "giftedGems": true,
				            "wonChallenge": true,
				            "newPM": true,
				            "unsubscribeFromAll": false
			            },
			            "emailNotifications": {
				            "onboarding": true,
				            "weeklyRecaps": true,
				            "importantAnnouncements": true,
				            "invitedQuest": true,
				            "questStarted": true,
				            "invitedGuild": true,
				            "invitedParty": true,
				            "giftedSubscription": true,
				            "giftedGems": true,
				            "wonChallenge": true,
				            "kickedGroup": true,
				            "newPM": true,
				            "unsubscribeFromAll": false
			            },
			            "webhooks": {
				
			            },
			            "displayInviteToPartyWhenPartyIs1": true,
			            "reverseChatOrder": false,
			            "toolbarCollapsed": false,
			            "advancedCollapsed": false,
			            "tagsCollapsed": false,
			            "dailyDueDefaultView": false,
			            "newTaskEdit": false,
			            "disableClasses": false,
			            "stickyHeader": true,
			            "sleep": true,
			            "dateFormat": "MM/dd/yyyy",
			            "autoEquip": true,
			            "allocationMode": "flat",
			            "chair": "none",
			            "sound": "rosstavoTheme",
			            "timezoneOffset": 420,
			            "shirt": "blue",
			            "skin": "915533",
			            "hideHeader": false,
			            "hair": {
				            "flower": 1,
				            "mustache": 0,
				            "beard": 0,
				            "bangs": 1,
				            "base": 3,
				            "color": "red"
			            },
			            "size": "slim",
			            "dayStart": 0
		            },
		            "party": {
			            "quest": {
				            "RSVPNeeded": false,
				            "progress": {
					            "collectedItems": 0,
					            "collect": {
						
					            },
					            "down": 0,
					            "up": 0
				            }
			            },
			            "orderAscending": "ascending",
			            "order": "level"
		            },
		            "guilds": [],
		            "invitations": {
			            "party": {
				
			            },
			            "guilds": []
		            },
		            "challenges": [],
		            "newMessages": {
			
		            },
		            "lastCron": "2017-07-30T04:26:41.358Z",
		            "items": {
			            "lastDrop": {
				            "count": 0,
				            "date": "2017-07-29T07:06:34.812Z"
			            },
			            "quests": {
				            "dustbunnies": 1
			            },
			            "mounts": {
				
			            },
			            "food": {
				
			            },
			            "hatchingPotions": {
				            "Desert": 1,
				            "RoyalPurple": 1
			            },
			            "eggs": {
				            "Wolf": 1
			            },
			            "pets": {
				
			            },
			            "special": {
				            "goodluckReceived": [],
				            "goodluck": 0,
				            "getwellReceived": [],
				            "getwell": 0,
				            "congratsReceived": [],
				            "congrats": 0,
				            "birthdayReceived": [],
				            "birthday": 0,
				            "thankyouReceived": [],
				            "thankyou": 0,
				            "greetingReceived": [],
				            "greeting": 0,
				            "nyeReceived": [],
				            "nye": 0,
				            "valentineReceived": [],
				            "valentine": 0,
				            "seafoam": 0,
				            "shinySeed": 0,
				            "spookySparkles": 0,
				            "snowball": 0
			            },
			            "gear": {
				            "costume": {
					            "shield": "shield_base_0",
					            "head": "head_base_0",
					            "armor": "armor_base_0"
				            },
				            "equipped": {
					            "weapon": "weapon_warrior_0",
					            "shield": "shield_base_0",
					            "head": "head_base_0",
					            "armor": "armor_base_0"
				            },
				            "owned": {
					            "armor_special_bardRobes": true,
					            "weapon_warrior_0": true,
					            "head_special_bardHat": true,
					            "eyewear_special_yellowTopFrame": true,
					            "eyewear_special_whiteTopFrame": true,
					            "eyewear_special_redTopFrame": true,
					            "eyewear_special_pinkTopFrame": true,
					            "eyewear_special_greenTopFrame": true,
					            "eyewear_special_blueTopFrame": true,
					            "eyewear_special_blackTopFrame": true
				            }
			            }
		            },
		            "history": {
			            "todos": [{
				            "value": -1,
				            "date": "2017-07-19T03:51:39.683Z"
			            },
			            {
				            "value": 0,
				            "date": "2017-07-19T23:05:15.436Z"
			            },
			            {
				            "value": 0,
				            "date": "2017-07-28T22:29:05.615Z"
			            },
			            {
				            "value": 0,
				            "date": "2017-07-30T04:26:41.358Z"
			            }],
			            "exp": [{
				            "value": 0,
				            "date": "2017-07-19T03:51:39.683Z"
			            },
			            {
				            "value": 66,
				            "date": "2017-07-19T23:05:15.436Z"
			            },
			            {
				            "value": 66,
				            "date": "2017-07-28T22:29:05.615Z"
			            },
			            {
				            "value": 312,
				            "date": "2017-07-30T04:26:41.358Z"
			            }]
		            },
		            "flags": {
			            "onboardingEmailsPhase": "7-b-1500922546651",
			            "warnedLowHealth": false,
			            "cardReceived": false,
			            "armoireEmpty": false,
			            "armoireOpened": false,
			            "armoireEnabled": true,
			            "welcomed": true,
			            "cronCount": 4,
			            "communityGuidelinesAccepted": false,
			            "lastWeeklyRecap": "2017-07-16T07:33:02.048Z",
			            "weeklyRecapEmailsPhase": 0,
			            "recaptureEmailsPhase": 0,
			            "levelDrops": {
				
			            },
			            "rebirthEnabled": false,
			            "classSelected": false,
			            "rewrite": true,
			            "newStuff": false,
			            "itemsEnabled": true,
			            "dropsEnabled": true,
			            "tutorial": {
				            "ios": {
					            "reorderTask": false,
					            "inviteParty": false,
					            "groupPets": false,
					            "filterTask": false,
					            "deleteTask": false,
					            "editTask": false,
					            "addTask": false
				            },
				            "common": {
					            "inbox": true,
					            "mounts": true,
					            "items": true,
					            "equipment": true,
					            "tavern": true,
					            "classes": true,
					            "skills": true,
					            "gems": true,
					            "pets": true,
					            "party": true,
					            "rewards": true,
					            "todos": true,
					            "dailies": true,
					            "habits": true
				            }
			            },
			            "tour": {
				            "equipment": -1,
				            "hall": -1,
				            "mounts": -1,
				            "pets": -1,
				            "market": -2,
				            "challenges": -1,
				            "guilds": -1,
				            "party": -2,
				            "tavern": -2,
				            "stats": -2,
				            "classes": -1,
				            "intro": -1
			            },
			            "showTour": true,
			            "customizationsNotification": true
		            },
		            "purchased": {
			            "plan": {
				            "dateUpdated": "2017-07-19T03:51:39.693Z",
				            "consecutive": {
					            "trinkets": 0,
					            "gemCapExtra": 0,
					            "offset": 0,
					            "count": 0
				            },
				            "mysteryItems": [],
				            "gemsBought": 0,
				            "extraMonths": 0,
				            "quantity": 1
			            },
			            "txnCount": 0,
			            "background": {
				            "violet": true,
				            "blue": true,
				            "green": true,
				            "purple": true,
				            "red": true,
				            "yellow": true
			            },
			            "shirt": {
				
			            },
			            "hair": {
				
			            },
			            "skin": {
				
			            },
			            "ads": false
		            },
		            "balance": 0,
		            "contributor": {
			
		            },
		            "backer": {
			
		            },
		            "achievements": {
			            "perfect": 0,
			            "quests": {
				
			            },
			            "challenges": [],
			            "ultimateGearSets": {
				            "warrior": false,
				            "rogue": false,
				            "wizard": false,
				            "healer": false
			            }
		            },
		            "_v": 447,
		            "auth": {
			            "timestamps": {
				            "loggedin": "2017-07-30T04:26:41.358Z",
				            "created": "2017-07-16T07:33:02.047Z"
			            },
			            "local": {
				            "username": "dotnet_test",
				            "lowerCaseUsername": "dotnet_test",
				            "email": "habitica-dotnet-test@outlook.com"
			            },
			            "google": {
				
			            },
			            "facebook": {
				
			            }
		            },
		            "id": "cfd9941b-037c-45bc-9d86-320067a4de3e",
		            "needsCron": false
	            },
	            "notifications": [{
		            "type": "CRON",
		            "data": {
			            "mp": 0,
			            "hp": 0
		            },
		            "id": "bc817158-da5b-49ae-ad63-b6d41eaeb629"
	            }]
            }

            */
        }
    }
}