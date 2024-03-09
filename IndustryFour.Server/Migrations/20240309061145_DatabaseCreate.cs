using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IndustryFour.Server.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "varchar(150)", nullable: false),
                    Author = table.Column<string>(type: "varchar(150)", nullable: false),
                    Description = table.Column<string>(type: "varchar(350)", nullable: true),
                    Content = table.Column<string>(type: "varchar(150)", nullable: false),
                    SourceUrl = table.Column<string>(type: "varchar(150)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Podcast Transcript" },
                    { 2, "LinkedIn Post" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "Description", "PublishDate", "SourceUrl", "Title" },
                values: new object[,]
                {
                    { 1, "Kudzai M", 1, " In this episode, we discuss the application of the unified namespace concept as a framework for digital transformation in manufacturing. We first take a back-to-basics approach, and then we dive deep into the details of how to implement this powerful concept for your digital transformation strategy. My guest on this episode is Hawker D. Reynolds. Hawker is the president and solutions architect at 4.0 Solutions, and board chairman at Intellic Integration based in Dallas, Texas. He coined and popularized the unified namespace concept in the context of industrial IoT. Hawker's career spans more than 20 years, with time spent in mining, steel, printing, and Tier 1 automotive, before transitioning to systems integration and ultimately becoming a business owner and educator. This episode is made possible by our friends at HiveMQ, who are providers of an enterprise MQTT platform. So please do check them out to help support this podcast. Welcome to the fourth generation podcast here on Industry 4.0 TV, which is a series of interviews designed to help you learn industrial IoT from some of the world's leading practitioners. So if you're new here, please do make sure to subscribe and click on the notification bell so that you never miss any of the interviews. Now, here is my interview with Hawker. Okay, Hawker, thank you so much for joining us on the show today. It's great to have you and I'm looking forward to having an interesting conversation with you. So welcome. Thank you for having me, man. Okay. So, I mean, as you might be aware, the unified namespace concept has been getting quite some traction lately, thanks to you. I mean, you've really kind of like really put the message out there. I mean, I mentioned this also on your podcast that you have been pounding this message for years and years. So I think it's kind of like really finally coming together. People are starting to kind of like realize that. So that's why I thought it's timely for us to actually have the conversation here to kind of like establish a back to basics of the unified namespace, you know, to kind of like lay the foundation for those who want to get a solid understanding of the concept. Perfect. Awesome. Yeah, you know, it's funny. I was having this conversation the other day talking about the unified namespace. And I told a story before about like, the very first unified namespace I ever built was completed in 2005. And it was built using dynamic data exchange. So DDE using Excel spreadsheets, and it was done in a salt mine. And that whole concept where I was, we had Data Highway Plus connecting every, you know, all of the infrastructure from the Stamblers, which break up all the salt, all the way down through the conveyor systems down to the screen plant going up through the hoist. Everything was Data Highway Plus. And if we were using, you know, we were all a Rockwell infrastructure. So there was a, there was the ability to acquire the data, like even over DH plus, we, it was slow, but you could still acquire it. Right. And one of the things that I thought I was young at that time, I mean, at that time, when I, I was, you know, in my early 20s, when this, when I first looked at the mine and went, it makes no sense to me that I have to drive down to the screen plant, walk into the control booth to look at the HMI in that control booth. I got to drive, you know, speed the most 15 miles an hour. And then I got to drive all the way up to each of the panels. Panels is where the mining team works. I have to drive up to the panel and I got to go to the, to the, the panel office, which is really nothing but a skid and to look at the HMI there to see the status of the Stambler. It made no sense that we did that. Right. And so I was like, what we really need to do is we need to unify all operations in this salt mine, which continues to extend by the way in mining, you know, you're never in one place too long in the, in the production environments, they continue to expand. You know, we were six, seven miles, you take skip down, you'd be six, seven miles, but 40, 40 years earlier, they were mining right where we came down at the bottom. And so I thought, well, the first thing I need to do is I need to connect to everything and put everything in one place. That was really the very first concept. And I said, how do I, how could I do that? And the answer was, I'll use, I'll use RS links to grab all the data over data highway plus, and then I'll just use a connector to get it all into Excel. And then once I've got it in Excel, I can use dynamic data exchange as my unified namespace. ISA 90, I didn't use ISA 95 then I would just use a, a concept of, Hey, I'm going to organize the business. So the parent company and then the division and then, you know, location, the site, and then the, and then break it up by area and then break it up by operational function. That's that was the first unified namespace in 2005. And I've been my entire career. That's all I've ever done. It wasn't until, you know, every solution I've ever built, whether it's for Starbucks or whether it's for Toyota or whether it's, you know, enter in other company name, I've always used UNS. The difference was I wasn't doing enterprise class solutions until 2013. And so my first enterprise class solution was 2013 and I, you know, I won a major award for it. We built the largest standalone SCADA system in the world. This is where we, why people listen to me. I, we built the largest standalone SCADA system in the world in 2013, you know, 11 million tags, 11 million data points, 2000 concurrent users, 2 million daily alarms, covering five States, 15,000 locations, 40,000 devices. We did that whole thing in 18 months for $1.6 million. And the next closest bid, which was Wonderware, by the way, was 50 million. The Rockwell bid was almost a hundred million. So we win this major award, you know, and we used Ignition to do it. And Ignition had to develop technology to make it even possible in 2013. Ignition had to develop the technology. So that was the gateway area network that you guys know now know as EAM, but it was originally gateway area network. Kepware also had to develop a, an API so that you could hit Kepware's Kepserver service and be able to configure devices from without having to go into their local client. There was technology that had to be developed and it was developed on that project. And then a year later, we were introduced to MQTT. So that, that big project that we did in 2013, there were limitations because it had to be OPC UA. So it was Modbus converted to OPC UA. And then we had to do all sorts of load balancing to make it even scalable across that network. And then we, and then Arlen Nipper presented the next year at, at ICC, the year after we won our award and, and everything changed. It was, it was like, oh, that's the solution. And so 2014 was when we really started scaling it. We did it with three huge companies. We did two, three huge global implementations. And then we were like, this is the way to do it. Everyone, what happened, companies started hiring us to, to come behind other integrators and do what we do. And so what I started observing was everyone's making the exact same mistakes. And we're like, okay, so we should start educating the community on this so that they don't make those mistakes. And that's where it all started. I mean, and, and so, yeah, I've been blowing the horn as loud as I possibly can since 2018. But anyway, that's the history of the UNS, if you were going to ask. Yeah, absolutely. Yeah, I was gonna ask, but it's really, really fascinating to hear the history of the, of the UNS and how it all came to be. So maybe let's kind of like take a step back a bit to define the term. How would you describe the unified namespace? And maybe in relation to like a traditional industrial architecture? Yeah, so the best way to describe a UNS is think, you know, we have to start with the automation stack. So if we start with the automation stack, I, one of the things I find is that most people who don't work in manufacturing, you know, I just had a meeting with an ERP company yesterday, Odoo, for example, I met with Odoo, who's one of their sales engineers. And he's an IT guy, lives in San Francisco, he's a software developer, he's never worked in manufacturing. One of the things that's very interesting is that people who don't work in manufacturing don't know how manufacturing actually works, right? So the automation stack is very much unique to manufacturing and industry, right? So it's basically five or six layers, we use a five layer stack, but there's essentially five or six layers, we're going to go ahead and use five layer stack. So on the very bottom, down on the plant floor, or the edge is PLC HMI. It's the automation of equipment, right? At the layer above that is supervisory control and data acquisition. That is the place where supervisors are controlled, monitor and manage automation of equipment. So supervisor control and data acquisition is a layer directly above that. The layer directly above that is the manufacturing execution layer. And that's really like the plant level. And that is where you are taking a sales order from the business and converting it into manufacturing. So MES is going to apply to an entire plant. And then the layer above that's ERP, which is really business, that's where IT starts. So at the IT layer, that's really where CRM, so the sales process, the planning of manufacturing, inventory management, getting paid, finance, all that kind of stuff. That's where all that starts. And then directly above that is cloud, that's the newest layer.", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.youtube.com/watch?v=dy1OcOCigmI", "Unified Namespace" },
                    { 2, "Walker Reynolds", 2, " Finalizing the lesson for tomorrow’s Mastermind session at iiot.university\r\n\r\nPart 2 of Big Data will follow up on last month’s session where we explained data lakes, warehouses and solutions.\r\n\r\nFor big data lesson we will be leveraging Kepware, a PTC Technology, Inductive Automation, HighByte, HiveMQ, Amazon Web Services (AWS), Snowflake and others.\r\n\r\nWe will also have a discussion about how to leverage FOSS and MING along side Commercial solutions. This will be the foundation for a deeper discussion on our podcast coming up next month.\r\n\r\nPlease check the Mastermind Channel in Discord for additional resources (I’ll include any supporting info there by 6pm CST this evening)\r\n\r\nSee you there!\r\n\r\nTentative Agenda:\r\n\r\n1. MING and FOSS\r\n2. UNS Geneaology Part 2\r\n3. HighByte Snowflake Connectors\r\n4. Big Data Integration Demo (UNS and your options)\r\n5. Decision Making with Big Data — How do I make the right call?", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.linkedin.com/posts/4point0solutions_activity-7171598828813447168-fuPG", "4.0 Solutions LinkedIn Post" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CategoryId",
                table: "Documents",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
