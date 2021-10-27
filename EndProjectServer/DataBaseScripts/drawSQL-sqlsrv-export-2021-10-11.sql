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
CREATE TABLE "Tag"(
    "ID" INT Identity NOT NULL,
    "PostId" INT NOT NULL,
    "Name" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Tag" ADD CONSTRAINT "tag_id_primary" PRIMARY KEY("ID");
CREATE TABLE "Comment"(
    "ID" INT Identity NOT NULL,
    "PostID" INT NOT NULL,
    "RepliedToID" INT NULL,
    "UserID" INT NOT NULL,
    "NumOfLikes" INT NOT NULL,
    "Text" NVARCHAR(255) NOT NULL,
    "TimeCreated" DATETIME NOT NULL
);
ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_id_primary" PRIMARY KEY("ID");
CREATE TABLE "Post"(
    "ID" INT Identity NOT NULL,
    "TopicID" INT NOT NULL,
    "UserID" INT NOT NULL,
    "NumOfLikes" INT NOT NULL,
    "Text" NVARCHAR(255) NOT NULL,
    "Image" NVARCHAR(255) NOT NULL,
    "Title" NVARCHAR(255) NOT NULL,
    "TimeCreated" DATETIME NOT NULL
);
ALTER TABLE
    "Post" ADD CONSTRAINT "post_id_primary" PRIMARY KEY("ID");
CREATE TABLE "TagsInPost"(
    "ID" INT Identity NOT NULL,
    "TagID" INT NOT NULL,
    "PostID" INT NOT NULL
);
ALTER TABLE
    "TagsInPost" ADD CONSTRAINT "tagsinpost_id_primary" PRIMARY KEY("ID");
CREATE TABLE "Review"(
    "ID" INT Identity NOT NULL,
    "TopicID" INT NOT NULL,
    "Score" INT NOT NULL,
    "UserID" INT NOT NULL,
    "Text" NVARCHAR(255) NOT NULL,
    "TimeCreated" DATETIME NOT NULL
);
ALTER TABLE
    "Review" ADD CONSTRAINT "review_id_primary" PRIMARY KEY("ID");
CREATE TABLE "LikesInComment"(
    "UserID" INT NOT NULL,
    "CommentID" INT NOT NULL,
    "IsLiked" BIT NOT NULL,
    "IsDisliked" BIT NOT NULL
);
ALTER TABLE
    "LikesInComment" ADD CONSTRAINT "likesincomment_userid_primary" PRIMARY KEY("UserID","CommentID");

CREATE TABLE "LikesInPost"(
    "UserID" INT NOT NULL,
    "PostID" INT NOT NULL,
    "IsLiked" BIT NOT NULL,
    "IsDisliked" BIT NOT NULL
);
ALTER TABLE
    "LikesInPost" ADD CONSTRAINT "likesinpost_userid_primary" PRIMARY KEY("UserID", "PostID");

ALTER TABLE
    "Post" ADD CONSTRAINT "post_topicid_foreign" FOREIGN KEY("TopicID") REFERENCES "Topic"("ID");
ALTER TABLE
    "Review" ADD CONSTRAINT "review_topicid_foreign" FOREIGN KEY("TopicID") REFERENCES "Topic"("ID");
ALTER TABLE
    "TagsInPost" ADD CONSTRAINT "tagsinpost_tagid_foreign" FOREIGN KEY("TagID") REFERENCES "Tag"("ID");
ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_postid_foreign" FOREIGN KEY("PostID") REFERENCES "Post"("ID");
ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_repliedtoid_foreign" FOREIGN KEY("RepliedToID") REFERENCES "Comment"("ID");
ALTER TABLE
    "Comment" ADD CONSTRAINT "comment_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");
ALTER TABLE
    "Post" ADD CONSTRAINT "post_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");
ALTER TABLE
    "TagsInPost" ADD CONSTRAINT "tagsinpost_postid_foreign" FOREIGN KEY("PostID") REFERENCES "Post"("ID");
ALTER TABLE
    "Review" ADD CONSTRAINT "review_userid_foreign" FOREIGN KEY("UserID") REFERENCES "User"("ID");

	
	
	
	
	Insert Into "User" VALUES ('a','b','c','11.10.2021', '0', '11.10.2021');