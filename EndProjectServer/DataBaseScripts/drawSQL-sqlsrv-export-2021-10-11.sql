create DATABASE EndProjectDB
go
Use EndProjectDB

CREATE TABLE "User"(
    "ID" INT Identity NOT NULL,
    "Name" NVARCHAR(255) NOT NULL,
    "Email" NVARCHAR(255) NOT NULL,
    "Password" NVARCHAR(255) NOT NULL,
    "DateCreated" DATETIME NOT NULL,
    "IsAdmin" BIT NOT NULL,
    "IsBanned" BIT NOT NULL,
    "BirthDate" DATE NOT NULL
);
ALTER TABLE
    "User" ADD CONSTRAINT "user_id_primary" PRIMARY KEY("ID");
CREATE TABLE "Topic"(
    "ID" INT Identity NOT NULL,
    "Name" NVARCHAR(255) NOT NULL,
    "AboutText" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Topic" ADD CONSTRAINT "topic_id_primary" PRIMARY KEY("ID");

CREATE TABLE "Comment"(
    "ID" INT Identity NOT NULL,
    "PostID" INT NOT NULL,
    "RepliedToID" INT NULL,
    "UserID" INT NOT NULL,
    "NumOfLikes" INT NOT NULL,
    "Text" TEXT NOT NULL,
    "TimeCreated" DATETIME NOT NULL
);
ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_id_primary" PRIMARY KEY("ID");
CREATE TABLE "Post"(
    "ID" INT Identity NOT NULL,
    "TopicID" INT NOT NULL,
    "UserID" INT NOT NULL,
    "NumOfLikes" INT NOT NULL,
    "Text" TEXT,
   
    "Title" NVARCHAR(255) NOT NULL,
    "TimeCreated" DATETIME NOT NULL
);
ALTER TABLE
    "Post" ADD CONSTRAINT "post_id_primary" PRIMARY KEY("ID");

CREATE TABLE "Review"(
    "ID" INT Identity NOT NULL,
    "TopicID" INT NOT NULL,
    "Score" DECIMAL NOT NULL,
    "UserID" INT NOT NULL,
    "Text" TEXT NOT NULL,
    "TimeCreated" DATETIME NOT NULL
);
ALTER TABLE
    "Review" ADD CONSTRAINT "review_id_primary" PRIMARY KEY("ID");
CREATE TABLE "LikesInComment"(
"ID" INT IDENTITY,
    "UserID" INT NOT NULL,
    "CommentID" INT NOT NULL,
    "IsLiked" BIT NOT NULL,
    "IsDisliked" BIT NOT NULL
);
ALTER TABLE
    "LikesInComment" ADD CONSTRAINT "likesincomment_userid_primary" PRIMARY KEY("ID");

CREATE TABLE "LikesInPost"(
"ID" INT IDENTITY,
    "UserID" INT NOT NULL,
    "PostID" INT NOT NULL,
    "IsLiked" BIT NOT NULL,
    "IsDisliked" BIT NOT NULL
);
ALTER TABLE
    "LikesInPost" ADD CONSTRAINT "likesinpost_id_primary" PRIMARY KEY("ID");

ALTER TABLE
    "Post" ADD CONSTRAINT "post_topicid_foreign" FOREIGN KEY("TopicID") REFERENCES "Topic"("ID");
ALTER TABLE
    "Review" ADD CONSTRAINT "review_topicid_foreign" FOREIGN KEY("TopicID") REFERENCES "Topic"("ID");

ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_postid_foreign" FOREIGN KEY("PostID") REFERENCES "Post"("ID");
ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_repliedtoid_foreign" FOREIGN KEY("RepliedToID") REFERENCES "Comment"("ID");
ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");
ALTER TABLE
    "Post" ADD CONSTRAINT "post_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");

ALTER TABLE
    "Review" ADD CONSTRAINT "review_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");
    ALTER TABLE
    "LikesInPost" ADD CONSTRAINT "likesinpost_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");
    ALTER TABLE
    "LikesInPost" ADD CONSTRAINT "likesinpost_postid_foreign" FOREIGN KEY("PostID") REFERENCES "Post"("ID");
     ALTER TABLE
    "LikesInComment" ADD CONSTRAINT "likesincoment_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");
    ALTER TABLE
    "LikesInComment" ADD CONSTRAINT "likesincomment_commentid_foreign" FOREIGN KEY("CommentID") REFERENCES "Comment"("ID");

	
	
	
	
	Insert Into "User" VALUES ('a','b','c','11.10.2021', '1','0', '11.10.2021');
    Insert INTO "Topic" ("Name", AboutText) VALUES('GTA 5', 'Grand Theft Auto V is a 2013 action-adventure game developed by Rockstar North and published by Rockstar Games. It is the seventh main entry in the Grand Theft Auto series, following 2008')
     Insert INTO "Topic" ("Name", AboutText) VALUES('Minecraft','Minecraft is a sandbox video game developed by Mojang Studios')
     Insert INTO "Topic" ("Name", AboutText) Values('Person 5','Persona 5 is a 2016 role-playing video game developed by Atlus. It is the sixth installment in the Persona series, which is part of the larger Megami Tensei franchise')