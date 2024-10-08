﻿using System.Collections.Generic;
using System.Linq;

namespace MediaFixerLib.Fixer
{
    public static class GratefulDeadTrackNameData
    {
        public static string Search(string term)
        {
            return TrackNamePairs
                .FirstOrDefault(kvp => term.ToLowerAlphaNumeric().Contains(kvp.Key))
                .Value;
        }

        #region Track Name Pairs
        private static readonly IEnumerable<KeyValuePair<string, string>> TrackNamePairs = new[]
        {
            KeyValuePair.Create("hullygully", "(Baby) Hully Gully"      ),
            KeyValuePair.Create("childrenoftheeighties", "(For The) Children Of The Eighties"),
            KeyValuePair.Create("satisfaction", "(I Can't Get No) Satisfaction"),
            KeyValuePair.Create("roadrunner", "(I'm A) Road Runner"),
            KeyValuePair.Create("avoicefromonhigh", "A Voice From On High"),
            KeyValuePair.Create("aintitcrazy", "Ain't It Crazy (The Rub)"),
            KeyValuePair.Create("therub", "Ain't It Crazy (The Rub)"),
            KeyValuePair.Create("alabamagetaway", "Alabama Getaway"),
            KeyValuePair.Create("alicedmillionaire", "Alice D Millionaire"),
            KeyValuePair.Create("allalongthewatchtower", "All Along The Watchtower"),
            KeyValuePair.Create("allihavetodoisdream", "All I Have To Do Is Dream"),
            KeyValuePair.Create("allofmylove", "All Of My Love"),
            KeyValuePair.Create("aligator", "Alligator"),
            KeyValuePair.Create("alligator", "Alligator"),
            KeyValuePair.Create("althea", "Althea"),
            KeyValuePair.Create("awbygn", "And We Bid You Goodnight"),
            KeyValuePair.Create("goodnight", "And We Bid You Goodnight"),
            KeyValuePair.Create("webidyou", "And We Bid You Goodnight"),
            KeyValuePair.Create("areyoulonelyforme", "Are You Lonely For Me"),
            KeyValuePair.Create("aroundand", "Around And Around"),
            KeyValuePair.Create("aroundaround", "Around And Around"),
            KeyValuePair.Create("atticsofmylife", "Attics Of My Life"),
            KeyValuePair.Create("babaoriley", "Baba O'Riley"),
            KeyValuePair.Create("babywhatyouwantmetodo", "Baby What You Want Me To Do"),
            KeyValuePair.Create("badmoonrising", "Bad Moon Rising"),
            KeyValuePair.Create("balladofathinman", "Ballad Of A Thin Man"),
            KeyValuePair.Create("balladofcaseyjones", "Ballad Of Casey Jones"),
            KeyValuePair.Create("bananaboatsongdayo", "Banana Boat Song (Day-O)"),
            KeyValuePair.Create("banksoftheohio", "Banks Of The Ohio"),
            KeyValuePair.Create("barbaraallen", "Barbara Allen"),
            KeyValuePair.Create("biodtl", "Beat It On Down The Line"),
            KeyValuePair.Create("beatitondown", "Beat It On Down The Line"),
            KeyValuePair.Create("beerbarrelpolka", "Beer Barrel Polka"),
            KeyValuePair.Create("believeitornot", "Believe It Or Not"),
            KeyValuePair.Create("bertha", "Bertha"),
            KeyValuePair.Create("betty", "Betty And Dupree"),
            KeyValuePair.Create("bigbossman", "Big Boss Man"),
            KeyValuePair.Create("bigboypete", "Big Boy Pete"),
            KeyValuePair.Create("bigbreasa", "Big Breasa"),
            KeyValuePair.Create("bigrailroadblues", "Big Railroad Blues"),
            KeyValuePair.Create("bigriver", "Big River"),
            KeyValuePair.Create("billgraham", "Bill Graham"),
            KeyValuePair.Create("birdsong", "Bird Song"),
            KeyValuePair.Create("blackmuddyriver", "Black Muddy River"),
            KeyValuePair.Create("blackpeter", "Black Peter"),
            KeyValuePair.Create("blackqueen", "Black Queen"),
            KeyValuePair.Create("blackthroated", "Black Throated Wind"),
            KeyValuePair.Create("blackbird", "Blackbird"),
            KeyValuePair.Create("blowaway", "Blow Away"),
            KeyValuePair.Create("bluemoon", "Blue Moon"),
            KeyValuePair.Create("bluesforallah", "Blues For Allah"),
            KeyValuePair.Create("borncrosseyed", "Born Cross-Eyed"),
            KeyValuePair.Create("bornonthebayou", "Born On The Bayou"),
            KeyValuePair.Create("boxofrain", "Box Of Rain"),
            KeyValuePair.Create("bringmemyshotgun", "Bring Me My Shotgun"),
            KeyValuePair.Create("brokedownpalace", "Brokedown Palace"),
            KeyValuePair.Create("brokenarrow", "Broken Arrow"),
            KeyValuePair.Create("brokenstring", "Broken String"),
            KeyValuePair.Create("browneyedwomen", "Brown-Eyed Women"),
            KeyValuePair.Create("builttolast", "Built To Last"),
            KeyValuePair.Create("byebyelove", "Bye Bye Love"),
            KeyValuePair.Create("ccrider", "C.C. Rider"),
            KeyValuePair.Create("californiaearthquake", "California Earthquake"),
            KeyValuePair.Create("cantcomedown", "Can't Come Down"),
            KeyValuePair.Create("candyman", "Candyman"),
            KeyValuePair.Create("cardboard", "Cardboard Cowboy"),
            KeyValuePair.Create("caseyjones", "Casey Jones"),
            KeyValuePair.Create("cassidy", "Cassidy"),
            KeyValuePair.Create("cathysclown", "Cathy's Clown"),
            KeyValuePair.Create("caution", "Caution (Do Not Stop On Tracks)"),
            KeyValuePair.Create("checkin", "Checkin' Up"),
            KeyValuePair.Create("childhoodsend", "Childhood's End"),
            KeyValuePair.Create("chimesoffreedom", "Chimes Of Freedom"),
            KeyValuePair.Create("chinacat", "China Cat Sunflower"),
            KeyValuePair.Create("chinadoll", "China Doll"),
            KeyValuePair.Create("chinatownshuffle", "Chinatown Shuffle"),
            KeyValuePair.Create("chinesebones", "Chinese Bones"),
            KeyValuePair.Create("clementine", "Clementine"),
            KeyValuePair.Create("closeencounters", "Close Encounters"),
            KeyValuePair.Create("cocainehabitblues", "Cocaine Habit Blues"),
            KeyValuePair.Create("coldjordon", "Cold Jordan"),
            KeyValuePair.Create("coldjordan", "Cold Jordan"),
            KeyValuePair.Create("crs", "Cold Rain And Snow"),
            KeyValuePair.Create("coldrain", "Cold Rain And Snow"),
            KeyValuePair.Create("comebackbaby", "Come Back Baby"),
            KeyValuePair.Create("comesatime", "Comes A Time"),
            KeyValuePair.Create("corrina", "Corrina"),
            KeyValuePair.Create("cosmic", "Cosmic Charlie"),
            KeyValuePair.Create("cowboysong", "Cowboy Song"),
            KeyValuePair.Create("crazyfingers", "Crazy Fingers"),
            KeyValuePair.Create("crowd", "Crowd"),
            KeyValuePair.Create("creampuffwar", "Cream Puff War"),
            KeyValuePair.Create("crypitical", "Cryptical Envelopment"),
            KeyValuePair.Create("cryptical", "Cryptical Envelopment"),
            KeyValuePair.Create("cumberlandblues", "Cumberland Blues"),
            KeyValuePair.Create("dancin", "Dancing In The Street"),
            KeyValuePair.Create("darkhollow", "Dark Hollow"),
            KeyValuePair.Create("darkstar", "Dark Star"),
            KeyValuePair.Create("darlingcorey", "Darling Corey"),
            KeyValuePair.Create("daytripper", "Day Tripper"),
            KeyValuePair.Create("daysbetween", "Days Between"),
            KeyValuePair.Create("deadman", "Dead Man, Dead Man"),
            KeyValuePair.Create("deal", "Deal"),
            KeyValuePair.Create("dearmrfantasy", "Dear Mr. Fantasy"),
            KeyValuePair.Create("deathdont", "Death Don't Have No Mercy"),
            KeyValuePair.Create("deathletterblues", "Death Letter Blues"),
            KeyValuePair.Create("deepelumblues", "Deep Elem Blues"),
            KeyValuePair.Create("deepelemblues", "Deep Elem Blues"),
            KeyValuePair.Create("desolationrow", "Desolation Row"),
            KeyValuePair.Create("devilwiththebluedress", "Devil With The Blue Dress"),
            KeyValuePair.Create("direwolf", "Dire Wolf"),
            KeyValuePair.Create("doyouwannadance", "Do You Wanna Dance"),
            KeyValuePair.Create("dointhatrag", "Doin' That Rag"),
            KeyValuePair.Create("donteasemein", "Don't Ease Me In"),
            KeyValuePair.Create("dontmessupagoodthing", "Don't Mess Up A Good Thing"),
            KeyValuePair.Create("dontneedlove", "Don't Need Love"),
            KeyValuePair.Create("dontthinktwiceitsallright", "Don't Think Twice It's All Right"),
            KeyValuePair.Create("downinthebottom", "Down In The Bottom"),
            KeyValuePair.Create("downsolong", "Down So Long"),
            KeyValuePair.Create("drinkup", "Drink Up And Go Home"),
            KeyValuePair.Create("drums", "Drums"),
            KeyValuePair.Create("rhythmdevils", "Drums"),
            KeyValuePair.Create("dupreesdiamondblues", "Dupree's Diamond Blues"),
            KeyValuePair.Create("earlymorningrain", "Early Morning Rain"),
            KeyValuePair.Create("easyanswers", "Easy Answers"),
            KeyValuePair.Create("easytoloveyou", "Easy To Love You"),
            KeyValuePair.Create("easywind", "Easy Wind"),
            KeyValuePair.Create("eighteenchildren", "Eighteen Children"),
            KeyValuePair.Create("elpaso", "El Paso"),
            KeyValuePair.Create("emptypages", "Empty Pages"),
            KeyValuePair.Create("equipment", "Equipment Problems"),
            KeyValuePair.Create("encorebreak", "Encore Break"),
            KeyValuePair.Create("estimatedprophet", "Estimated Prophet"),
            KeyValuePair.Create("eternity", "Eternity"),
            KeyValuePair.Create("everytimeyougoaway", "Every Time You Go Away"),
            KeyValuePair.Create("eyesoftheworld", "Eyes Of The World"),
            KeyValuePair.Create("farfromme", "Far From Me"),
            KeyValuePair.Create("feedback", "Feedback"),
            KeyValuePair.Create("feellikeastranger", "Feel Like A Stranger"),
            KeyValuePair.Create("fever", "Fever"),
            KeyValuePair.Create("funiculi", "Funiculi Funicula"),
            KeyValuePair.Create("fireonthe", "Fire On The Mountain"),
            KeyValuePair.Create("foolishheart", "Foolish Heart"),
            KeyValuePair.Create("foreveryoung", "Forever Young"),
            KeyValuePair.Create("foxyladyjam", "Foxy Lady Jam"),
            KeyValuePair.Create("franklinstower", "Franklin's Tower"),
            KeyValuePair.Create("friendofthe", "Friend Of The Devil"),
            KeyValuePair.Create("fromtheheartofme", "From The Heart Of Me"),
            KeyValuePair.Create("frozenlogger", "The Frozen Logger"),
            KeyValuePair.Create("gamespeopleplay", "Games People Play"),
            KeyValuePair.Create("gangsteroflove", "Gangster Of Love"),
            KeyValuePair.Create("bouquet", "Gathering Flowers For The Master's Bouquet"),
            KeyValuePair.Create("gentlemenstartyourengines", "Gentlemen, Start Your Engines"),
            KeyValuePair.Create("getback", "Get Back"),
            KeyValuePair.Create("gimmesomelovin", "Gimme Some Lovin'"),
            KeyValuePair.Create("gloria", "Gloria"),
            KeyValuePair.Create("gdtrfb", "Goin' Down The Road Feeling Bad"),
            KeyValuePair.Create("goin", "Goin' Down The Road Feeling Bad"),
            KeyValuePair.Create("goodgollymissmolly", "Good Golly Miss Molly"),
            KeyValuePair.Create("goodlovin", "Good Lovin'"),
            KeyValuePair.Create("goodmorning", "Good Morning Little Schoolgirl"),
            KeyValuePair.Create("schoolgirl", "Good Morning Little Schoolgirl"),
            KeyValuePair.Create("goodnightirene", "Goodnight Irene"),
            KeyValuePair.Create("goodvibrations", "Good Vibrations"),
            KeyValuePair.Create("gotmymojoworking", "Got My Mojo Working"),
            KeyValuePair.Create("gottaservesomebody", "Gotta Serve Somebody"),
            KeyValuePair.Create("greateststoryevertold", "Greatest Story Ever Told"),
            KeyValuePair.Create("greengreengrassofhome", "Green Green Grass Of Home"),
            KeyValuePair.Create("greenonions", "Green Onions"),
            KeyValuePair.Create("greenriver", "Green River"),
            KeyValuePair.Create("happinessisdrumming", "Happiness Is Drumming"),
            KeyValuePair.Create("h2h", "Hard To Handle"),
            KeyValuePair.Create("hardtohandle", "Hard To Handle"),
            KeyValuePair.Create("hewasafriendofmine", "He Was A Friend Of Mine"),
            KeyValuePair.Create("hesgone", "He's Gone"),
            KeyValuePair.Create("headsup", "Heads Up"),
            KeyValuePair.Create("heartofmine", "Heart Of Mine"),
            KeyValuePair.Create("heavenhelpthefool", "Heaven Help The Fool"),
            KeyValuePair.Create("hellinabucket", "Hell In A Bucket"),
            KeyValuePair.Create("helpmerhonda", "Help Me Rhonda"),
            KeyValuePair.Create("helpontheway", "Help On The Way"),
            KeyValuePair.Create("herecomessunshine", "Here Comes Sunshine"),
            KeyValuePair.Create("heybodiddley", "Hey Bo Diddley"),
            KeyValuePair.Create("heyjude", "Hey Jude"),
            KeyValuePair.Create("heylittleone", "Hey Little One"),
            KeyValuePair.Create("heypockyway", "Hey Pocky Way"),
            KeyValuePair.Create("hiheelsneakers", "Hi Heeled Sneakers"),
            KeyValuePair.Create("hiheeledsneakers", "Hi Heeled Sneakers"),
            KeyValuePair.Create("hideaway", "Hide Away"),
            KeyValuePair.Create("hightime", "High Time"),
            KeyValuePair.Create("highway61revisited", "Highway 61 Revisited"),
            KeyValuePair.Create("howlongblues", "How Long Blues"),
            KeyValuePair.Create("howsweet", "How Sweet It Is (To Be Loved by You)"),
            KeyValuePair.Create("iaintsuperstitious", "I Ain't Superstitious"),
            KeyValuePair.Create("ifoughtthelaw", "I Fought The Law"),
            KeyValuePair.Create("igetaround", "I Get Around"),
            KeyValuePair.Create("igotamindtogiveuplivin", "I Got A Mind To Give Up Livin'"),
            KeyValuePair.Create("ijustwannamakelovetoyou", "I Just Wanna Make Love To You"),
            KeyValuePair.Create("iknowitsasin", "I Know It's A Sin"),
            KeyValuePair.Create("yourrider", "I Know You Rider"),
            KeyValuePair.Create("iknowyourider", "I Know You Rider"),
            KeyValuePair.Create("ineedamiracle", "I Need A Miracle"),
            KeyValuePair.Create("isecondthatemotion", "I Second That Emotion"),
            KeyValuePair.Create("iwanttotellyou", "I Want To Tell You"),
            KeyValuePair.Create("iwantyou", "I Want You"),
            KeyValuePair.Create("iwashedmyhandsinmuddywater", "I Washed My Hands In Muddy Water"),
            KeyValuePair.Create("iwilltakeyouhome", "I Will Take You Home"),
            KeyValuePair.Create("illbeyourbabytonight", "I'll Be Your Baby Tonight"),
            KeyValuePair.Create("illgocrazy", "I'll Go Crazy"),
            KeyValuePair.Create("imahogforyoubaby", "I'm A Hog For You Baby"),
            KeyValuePair.Create("kingbee", "I'm A King Bee"),
            KeyValuePair.Create("imaman", "I'm A Man"),
            KeyValuePair.Create("aroundthisworld", "I've Been All Around This World"),
            KeyValuePair.Create("ivegotatigerbythetail", "I've Got A Tiger By The Tail"),
            KeyValuePair.Create("ivejustseenaface", "I've Just Seen A Face"),
            KeyValuePair.Create("iveseenthemall", "I've Seen Them All"),
            KeyValuePair.Create("ifihadtheworldtogive", "If I Had The World To Give"),
            KeyValuePair.Create("iftheshoefits", "If The Shoe Fits"),
            KeyValuePair.Create("aiko", "Iko Iko"),
            KeyValuePair.Create("ikoiko", "Iko Iko"),
            KeyValuePair.Create("midnighthour", "In The Midnight Hour"),
            KeyValuePair.Create("inthepines", "In The Pines"),
            KeyValuePair.Create("intro", "Introduction"),
            KeyValuePair.Create("hurtsmetoo", "It Hurts Me Too"),
            KeyValuePair.Create("musthavebeenthe", "It Must Have Been The Roses"),
            KeyValuePair.Create("traintocry", "It Takes A Lot to Laugh, It Takes A Train to Cry"),
            KeyValuePair.Create("mansworld", "It's A Man's, Man's, Man's World"),
            KeyValuePair.Create("itsasin", "It's A Sin"),
            KeyValuePair.Create("babyblue", "It's All Over Now, Baby Blue"),
            KeyValuePair.Create("itsmyownfault", "It's My Own Fault"),
            KeyValuePair.Create("jackaroe", "Jack A Roe"),
            KeyValuePair.Create("jackstraw", "Jack Straw"),
            KeyValuePair.Create("jam", "Jam"),
            KeyValuePair.Create("joey", "Joey"),
            KeyValuePair.Create("johnbrown", "John Brown"),
            KeyValuePair.Create("johnsother", "John's Other"),
            KeyValuePair.Create("johnnybgoode", "Johnny B. Goode"),
            KeyValuePair.Create("justalittlelight", "Just A Little Light"),
            KeyValuePair.Create("tomthumbblues", "Just Like Tom Thumb Blues"),
            KeyValuePair.Create("kansascity", "Kansas City"),
            KeyValuePair.Create("katiemae", "Katie Mae"),
            KeyValuePair.Create("keepongrowing", "Keep On Growing"),
            KeyValuePair.Create("keeprollingby", "Keep Rolling By"),
            KeyValuePair.Create("keepyourdayjob", "Keep Your Day Job"),
            KeyValuePair.Create("kingsolomonsmarbles", "King Solomon's Marbles"),
            KeyValuePair.Create("knockinonheavensdoor", "Knockin' On Heaven's Door"),
            KeyValuePair.Create("lalhambra", "L'Alhambra"),
            KeyValuePair.Create("labamba", "La Bamba"),
            KeyValuePair.Create("ladydi", "Lady Di And I"),
            KeyValuePair.Create("lazylightnin", "Lazy Lightning"),
            KeyValuePair.Create("lazyriverroad", "Lazy River Road"),
            KeyValuePair.Create("leaveyourloveathome", "Leave Your Love At Home"),
            KeyValuePair.Create("letitbeme", "Let It Be Me"),
            KeyValuePair.Create("letitgrow", "Let It Grow"),
            KeyValuePair.Create("letitrock", "Let It Rock"),
            KeyValuePair.Create("letmein", "Let Me In"),
            KeyValuePair.Create("letmesingyourbluesaway", "Let Me Sing Your Blues Away"),
            KeyValuePair.Create("letthegoodtimesroll", "Let The Good Times Roll"),
            KeyValuePair.Create("liberty", "Liberty"),
            KeyValuePair.Create("lindy", "Lindy"),
            KeyValuePair.Create("littleredrooster", "Little Red Rooster"),
            KeyValuePair.Create("littlesadie", "Little Sadie"),
            KeyValuePair.Create("littlestar", "Little Star"),
            KeyValuePair.Create("longblacklimousine", "Long Black Limousine"),
            KeyValuePair.Create("lookonyonderswall", "Look On Yonder's Wall"),
            KeyValuePair.Create("lookslikerain", "Looks Like Rain"),
            KeyValuePair.Create("looselucy", "Loose Lucy"),
            KeyValuePair.Create("loser", "Loser"),
            KeyValuePair.Create("lostsailor", "Lost Sailor"),
            KeyValuePair.Create("louielouie", "Louie, Louie"),
            KeyValuePair.Create("lovetheoneyourewith", "Love The One You're With"),
            KeyValuePair.Create("luciferseyes", "Lucifer's Eyes"),
            KeyValuePair.Create("lucyintheskywithdiamonds", "Lucy In The Sky with Diamonds"),
            KeyValuePair.Create("macktheknife", "Mack The Knife"),
            KeyValuePair.Create("maggiesfarm", "Maggie's Farm"),
            KeyValuePair.Create("mamatried", "Mama Tried"),
            KeyValuePair.Create("manofpeace", "Man Of Peace"),
            KeyValuePair.Create("womenaresmarter", "Man Smart (Woman Smarter)"),
            KeyValuePair.Create("mansmartwomansmarter", "Man Smart (Woman Smarter)"),
            KeyValuePair.Create("imaman", "Mannish Boy (I'm A Man)"),
            KeyValuePair.Create("mannishboy", "Mannish Boy (I'm A Man)"),
            KeyValuePair.Create("marriottusa", "Marriott USA"),
            KeyValuePair.Create("masonschildren", "Mason's Children"),
            KeyValuePair.Create("matildamatilda", "Matilda, Matilda"),
            KeyValuePair.Create("maybeyouknow", "Maybe You Know"),
            KeyValuePair.Create("bobbymcgee", "Me And Bobby McGee"),
            KeyValuePair.Create("myuncle", "Me And My Uncle"),
            KeyValuePair.Create("memphisblues", "Memphis Blues"),
            KeyValuePair.Create("mexacali", "Mexicali Blues"),
            KeyValuePair.Create("mexicali", "Mexicali Blues"),
            KeyValuePair.Create("mightaswell", "Might As Well"),
            KeyValuePair.Create("milkintheturkey", "King Solomon's Marbles"),
            KeyValuePair.Create("strongerthandirt", "King Solomon's Marbles"),
            KeyValuePair.Create("kingsolomonsmarbles", "King Solomon's Marbles"),
            KeyValuePair.Create("mindleftbody", "Mind Left Body"),
            KeyValuePair.Create("confusionsprince", "Mindbender (Confusion's Prince)"),
            KeyValuePair.Create("mindbender", "Mindbender (Confusion's Prince)"),
            KeyValuePair.Create("missionintherain", "Mission In The Rain"),
            KeyValuePair.Create("mississippi", "Mississippi Half-Step Uptown Toodleloo"),
            KeyValuePair.Create("halfstep", "Mississippi Half-Step Uptown Toodleloo"),
            KeyValuePair.Create("mrcharlie", "Mr. Charlie"),
            KeyValuePair.Create("mona", "Mona"),
            KeyValuePair.Create("moneymoney", "Money Money"),
            KeyValuePair.Create("monkey", "Monkey And The Engineer"),
            KeyValuePair.Create("morningdew", "Morning Dew"),
            KeyValuePair.Create("mountainsofthemoon", "Mountains Of The Moon"),
            KeyValuePair.Create("mountain", "Mountain"),
            KeyValuePair.Create("mrtambourineman", "Mr Tambourine Man"),
            KeyValuePair.Create("mybabe", "My Babe"),
            KeyValuePair.Create("mybabyleftme", "My Baby Left Me"),
            KeyValuePair.Create("mybrotheresau", "My Brother Esau"),
            KeyValuePair.Create("mysterytrain", "Mystery Train"),
            KeyValuePair.Create("neighborhoodgirls", "Neighborhood Girls"),
            KeyValuePair.Create("nevertrustawoman", "Never Trust A Woman"),
            KeyValuePair.Create("minglwood", "New Minglewood Blues"),
            KeyValuePair.Create("minglewood", "New Minglewood Blues"),
            KeyValuePair.Create("neworleans", "New Orleans"),
            KeyValuePair.Create("newpotatocaboose", "New Potato Caboose"),
            KeyValuePair.Create("newspeedwayboogie", "New Speedway Boogie"),
            KeyValuePair.Create("newyearscount", "New Year's Countdown"),
            KeyValuePair.Create("timeyousee", "Next Time You See Me"),
            KeyValuePair.Create("faultbutmine", "Nobody's Fault But Mine"),
            KeyValuePair.Create("notfadeaway", "Not Fade Away"),
            KeyValuePair.Create("odeforbilliedean", "Ode For Billie Dean"),
            KeyValuePair.Create("ohbabeitaintnolie", "Oh Babe, It Ain't No Lie"),
            KeyValuePair.Create("ohboy", "Oh Boy"),
            KeyValuePair.Create("okiefrommuskogee", "Okie From Muskogee"),
            KeyValuePair.Create("olslewfoot", "Ol' Slewfoot"),
            KeyValuePair.Create("oldoldhouse", "Old, Old House"),
            KeyValuePair.Create("arageed", "Ollin Arageed"),
            KeyValuePair.Create("arrageed", "Ollin Arageed"),
            KeyValuePair.Create("ontheroadagain", "On The Road Again"),
            KeyValuePair.Create("onekindfavor", "One Kind Favor"),
            KeyValuePair.Create("onemoresaturday", "One More Saturday Night"),
            KeyValuePair.Create("onlyafool", "Only A Fool"),
            KeyValuePair.Create("operator", "Operator"),
            KeyValuePair.Create("overseasstomp", "Overseas Stomp (Lindbergh Hop)"),
            KeyValuePair.Create("lindberghhop", "Overseas Stomp (Lindbergh Hop)"),
            KeyValuePair.Create("paininmyheart", "Pain In My Heart"),
            KeyValuePair.Create("parchmanfarm", "Parchman Farm"),
            KeyValuePair.Create("passenger", "Passenger"),
            KeyValuePair.Create("peggysue", "Peggy Sue"),
            KeyValuePair.Create("peggyo", "Peggy-O"),
            KeyValuePair.Create("philsearthquakespace", "Phil's Earthquake Space"),
            KeyValuePair.Create("philsolo", "Phil Solo"),
            KeyValuePair.Create("picassomoon", "Picasso Moon"),
            KeyValuePair.Create("playinreprise", "Playin' Reprise"),
            KeyValuePair.Create("playin", "Playing In The Band"),
            KeyValuePair.Create("pollution", "Pollution"),
            KeyValuePair.Create("prisonerblues", "Prisoner Blues"),
            KeyValuePair.Create("proudmary", "Proud Mary"),
            KeyValuePair.Create("queenjaneapproximately", "Queen Jane Approximately"),
            KeyValuePair.Create("radio", "Radio Announcers"),
            KeyValuePair.Create("railroadingonthegreatdivide", "Railroading On The Great Divide"),
            KeyValuePair.Create("rain", "Rain"),
            KeyValuePair.Create("rainyday", "Rainy Day Women # 12 & 35"),
            KeyValuePair.Create("ramble", "Ramble On Rose"),
            KeyValuePair.Create("reuben", "Reuben And Cerise"),
            KeyValuePair.Create("revolution", "Revolution"),
            KeyValuePair.Create("revolutionaryhamstrungblues", "Revolutionary Hamstrung Blues"),
            KeyValuePair.Create("riotincellblock", "Riot In Cell Block Number Nine"),
            KeyValuePair.Create("ripple", "Ripple"),
            KeyValuePair.Create("roberta", "Roberta"),
            KeyValuePair.Create("pneumonia", "Rockin' Pneumonia And Boogie Woogie Flu"),
            KeyValuePair.Create("tumblin", "Rollin' And Tumblin'"),
            KeyValuePair.Create("rosaliemcfall", "Rosa Lee McFall"),
            KeyValuePair.Create("rosaleemcfall", "Rosa Lee McFall"),
            KeyValuePair.Create("jimmy", "Row Jimmy"),
            KeyValuePair.Create("runrudolphrun", "Run, Rudolph, Run"),
            KeyValuePair.Create("sage", "Sage And Spirit"),
            KeyValuePair.Create("saintofcircumstance", "Saint Of Circumstance"),
            KeyValuePair.Create("stephen", "Saint Stephen"),
            KeyValuePair.Create("saltlakecity", "Salt Lake City"),
            KeyValuePair.Create("sambaintherain", "Samba In The Rain"),
            KeyValuePair.Create("samson", "Samson And Delilah"),
            KeyValuePair.Create("sawmill", "Sawmill"),
            KeyValuePair.Create("saybossman", "Say Boss Man"),
            KeyValuePair.Create("scarletbegonias", "Scarlet Begonias"),
            KeyValuePair.Create("searchin", "Searchin'"),
            KeyValuePair.Create("seasonsofmyheart", "Seasons Of My Heart"),
            KeyValuePair.Create("seasons", "Seasons"),
            KeyValuePair.Create("philandned", "Seastones"),
            KeyValuePair.Create("seastones", "Seastones"),
            KeyValuePair.Create("sgtpeppersband", "Sgt Pepper's Band"),
            KeyValuePair.Create("shakedownstreet", "Shakedown Street"),
            KeyValuePair.Create("shebelongstome", "She Belongs To Me"),
            KeyValuePair.Create("shesmine", "She's Mine"),
            KeyValuePair.Create("shelterfromthestorm", "Shelter From The Storm"),
            KeyValuePair.Create("shipoffools", "Ship Of Fools"),
            KeyValuePair.Create("sicktired", "Sick And Tired"),
            KeyValuePair.Create("sickandtired", "Sick And Tired"),
            KeyValuePair.Create("sidewalksofnewyork", "Sidewalks Of New York"),
            KeyValuePair.Create("silverthreads", "Silver Threads And Golden Needles"),
            KeyValuePair.Create("simpletwistoffate", "Simple Twist Of Fate"),
            KeyValuePair.Create("singmebackhome", "Sing Me Back Home"),
            KeyValuePair.Create("sittinontopofthe", "Sittin' On Top Of The World"),
            KeyValuePair.Create("slewfoot", "Slewfoot"),
            KeyValuePair.Create("slipknot", "Slipknot!"),
            KeyValuePair.Create("slowtrain", "Slow Train"),
            KeyValuePair.Create("smokestack", "Smokestack Lightning"),
            KeyValuePair.Create("somanyroads", "So Many Roads"),
            KeyValuePair.Create("sosad", "So Sad (To Watch Good Love Go Bad)"),
            KeyValuePair.Create("sowhat", "So What"),
            KeyValuePair.Create("spacefunk", "Space Funk"),
            KeyValuePair.Create("space", "Space"),
            KeyValuePair.Create("spanish", "Spanish"),
            KeyValuePair.Create("spoonful", "Spoonful"),
            KeyValuePair.Create("announcements", "Stage Announcements"),
            KeyValuePair.Create("banter", "Stage Banter"),
            KeyValuePair.Create("staggerlee", "Stagger Lee"),
            KeyValuePair.Create("standeronthemountain", "Stander On The Mountain"),
            KeyValuePair.Create("standingonthecorner", "Standing On The Corner"),
            KeyValuePair.Create("standingonthemoon", "Standing On The Moon"),
            KeyValuePair.Create("starsandstripes", "Stars And Stripes Forever"),
            KeyValuePair.Create("forever tuning", "Stars And Stripes Forever"),
            KeyValuePair.Create("stealin", "Stealin'"),
            KeyValuePair.Create("stellablue", "Stella Blue"),
            KeyValuePair.Create("stiritup", "Stir It Up"),
            KeyValuePair.Create("strongerthandirt", "Stronger Than Dirt"),
            KeyValuePair.Create("sugarmagnolia", "Sugar Magnolia"),
            KeyValuePair.Create("sugaee", "Sugaree"),
            KeyValuePair.Create("sugaree", "Sugaree"),
            KeyValuePair.Create("sunrise", "Sunrise"),
            KeyValuePair.Create("sunshinedaydream", "Sunshine Daydream"),
            KeyValuePair.Create("supplication", "Supplication"),
            KeyValuePair.Create("swinglowsweetchariot", "Swing Low Sweet Chariot"),
            KeyValuePair.Create("takeastepback", "Take A Step Back"),
            KeyValuePair.Create("takeitalloff", "Take It All Off"),
            KeyValuePair.Create("takemetotheriver", "Take Me To The River"),
            KeyValuePair.Create("tangledupinblue", "Tangled Up In Blue"),
            KeyValuePair.Create("tastebud", "Tastebud"),
            KeyValuePair.Create("monitor", "Technical Difficulties"),
            KeyValuePair.Create("techproblems", "Technical Difficulties"),
            KeyValuePair.Create("technical", "Technical Difficulties"),
            KeyValuePair.Create("tellmama", "Tell Mama"),
            KeyValuePair.Create("tennesee", "Tennessee Jed"),
            KeyValuePair.Create("tennessee", "Tennessee Jed"),
            KeyValuePair.Create("terrapinstation", "Terrapin Station"),
            KeyValuePair.Create("thatwouldbesomething", "That Would Be Something"),
            KeyValuePair.Create("thatllbetheday", "That'll Be The Day"),
            KeyValuePair.Create("thatsallrightmama", "That's All Right, Mama"),
            KeyValuePair.Create("frankielee", "The Ballad Of Frankie Lee And Judas Priest"),
            KeyValuePair.Create("judaspriest", "The Ballad Of Frankie Lee And Judas Priest"),
            KeyValuePair.Create("boxer", "The Boxer"),
            KeyValuePair.Create("eleven", "The Eleven"),
            KeyValuePair.Create("williamtell", "The Eleven"),
            KeyValuePair.Create("flood", "The Flood"),
            KeyValuePair.Create("frozenlogger", "The Frozen Logger"),
            KeyValuePair.Create("goldenroad", "The Golden Road (To Unlimited Devotion)"),
            KeyValuePair.Create("lasttime", "The Last Time"),
            KeyValuePair.Create("mainten", "The Main Ten"),
            KeyValuePair.Create("quinntheeskimo", "The Mighty Quinn (Quinn The Eskimo)"),
            KeyValuePair.Create("mightyquinn", "The Mighty Quinn (Quinn The Eskimo)"),
            KeyValuePair.Create("tmns", "The Music Never Stopped"),
            KeyValuePair.Create("musicnever", "The Music Never Stopped"),
            KeyValuePair.Create("oneyoulove", "The One You Love"),
            KeyValuePair.Create("onlytimeisnow", "The Only Time Is Now"),
            KeyValuePair.Create("fortheotherone", "That's It For The Other One"),
            KeyValuePair.Create("otherone", "The Other One"),
            KeyValuePair.Create("promisedland", "The Promised Land"),
            KeyValuePair.Create("raceison", "The Race Is On"),
            KeyValuePair.Create("ravenspace", "The Raven Space"),
            KeyValuePair.Create("samething", "The Same Thing"),
            KeyValuePair.Create("seven", "The Seven"),
            KeyValuePair.Create("thestranger", "The Stranger (Two Souls In Communion)"),
            KeyValuePair.Create("twosoulsincommunion", "The Stranger (Two Souls In Communion)"),
            KeyValuePair.Create("thingsiusedtodo", "The Things I Used To Do"),
            KeyValuePair.Create("timestheyareachanging", "The Times They Are A Changing"),
            KeyValuePair.Create("valleyroad", "The Valley Road"),
            KeyValuePair.Create("weight", "The Weight"),
            KeyValuePair.Create("wheel", "The Wheel"),
            KeyValuePair.Create("wickedmessenger", "The Wicked Messenger"),
            KeyValuePair.Create("thereissomethingonyourmind", "There Is Something On Your Mind"),
            KeyValuePair.Create("tleo", "They Love Each Other"),
            KeyValuePair.Create("theyloveeachother", "They Love Each Other"),
            KeyValuePair.Create("throwingstones", "Throwing Stones"),
            KeyValuePair.Create("tillthemorningcomes", "Till The Morning Comes"),
            KeyValuePair.Create("tolaymedown", "To Lay Me Down"),
            KeyValuePair.Create("tomorrowisalongtime", "Tomorrow Is A Long Time"),
            KeyValuePair.Create("tomorrowisforever", "Tomorrow Is Forever"),
            KeyValuePair.Create("tomorrowneverknows", "Tomorrow Never Knows"),
            KeyValuePair.Create("tonsofsteel", "Tons Of Steel"),
            KeyValuePair.Create("touchofgrey", "Touch Of Grey"),
            KeyValuePair.Create("truckin", "Truckin'"),
            KeyValuePair.Create("turnon", "Turn On Your Love Light"),
            KeyValuePair.Create("lovelight", "Turn On Your Love Light"),
            KeyValuePair.Create("twistshout", "Twist And Shout"),
            KeyValuePair.Create("twistandshout", "Twist And Shout"),
            KeyValuePair.Create("tuningjam", "Tuning Jam"),
            KeyValuePair.Create("tunin", "Tuning"),
            KeyValuePair.Create("usblues", "U.S. Blues"),
            KeyValuePair.Create("unbrokenchain", "Unbroken Chain"),
            KeyValuePair.Create("unclejohnsband", "Uncle John's Band"),
            KeyValuePair.Create("unclesamsblues", "Uncle Sam's Blues"),
            KeyValuePair.Create("unknownblues", "Unknown Blues"),
            KeyValuePair.Create("victimorthecrime", "Victim Or The Crime"),
            KeyValuePair.Create("violalee", "Viola Lee Blues"),
            KeyValuePair.Create("visionsofjohanna", "Visions Of Johanna"),
            KeyValuePair.Create("wabashcannonball", "Wabash Cannonball"),
            KeyValuePair.Create("littlesusie", "Wake Up Little Susie"),
            KeyValuePair.Create("walkdownthestreet", "Walk Down The Street"),
            KeyValuePair.Create("walkinblues", "Walkin' Blues"),
            KeyValuePair.Create("walkingthedog", "Walking The Dog"),
            KeyValuePair.Create("wangdangdoodle", "Wang Dang Doodle"),
            KeyValuePair.Create("warriorsofthesun", "Warriors Of The Sun"),
            KeyValuePair.Create("watchingtheriverflow", "Watching The River Flow"),
            KeyValuePair.Create("wavethatflag", "Wave That Flag"),
            KeyValuePair.Create("wavetothewind", "Wave To The Wind"),
            KeyValuePair.Create("waytogohome", "Way To Go Home"),
            KeyValuePair.Create("wecanrun", "We Can Run"),
            KeyValuePair.Create("prelude", "Weather Report Suite Prelude"),
            KeyValuePair.Create("weatherreport", "Weather Report Suite"),
            KeyValuePair.Create("werewolves", "Werewolves Of London"),
            KeyValuePair.Create("westlafadeaway", "West L.A. Fadeaway"),
            KeyValuePair.Create("warfrat", "Wharf Rat"),
            KeyValuePair.Create("wharfrat", "Wharf Rat"),
            KeyValuePair.Create("whatsbecomeofthebaby", "What's Become Of The Baby?"),
            KeyValuePair.Create("whatsgoingon", "What's Going On?"),
            KeyValuePair.Create("masterpiece", "When I Paint My Masterpiece"),
            KeyValuePair.Create("whenpushcomestoshove", "When Push Comes to Shove"),
            KeyValuePair.Create("whodoyoulove", "Who Do You Love?"),
            KeyValuePair.Create("whoslovin", "Who's Loving You Tonight?"),
            KeyValuePair.Create("whydontwedoitintheroad", "Why Don't We Do It In The Road?"),
            KeyValuePair.Create("willthecirclebeunbroken", "Will The Circle Be Unbroken?"),
            KeyValuePair.Create("handjive", "Willie And The Hand Jive"),
            KeyValuePair.Create("wowwowheyhey", "Wow Wow Hey Hey"),
            KeyValuePair.Create("yellowdogstory", "Yellow Dog Story"),
            KeyValuePair.Create("youaintwomanenough", "You Ain't Woman Enough"),
            KeyValuePair.Create("youdonthavetoask", "You Don't Have To Ask"),
            KeyValuePair.Create("youdontloveme", "You Don't Love Me"),
            KeyValuePair.Create("youwinagain", "You Win Again"),
            KeyValuePair.Create("yourloveathome", "Your Love At Home")
        };
        #endregion
    }
}