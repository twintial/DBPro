create table DBUser(
  userID varchar2(15) primary key,
  userName varchar2(15),
  userGender char(1),
  userLevel int,
  userIconID varchar2(20)
  );


create table DBuserInfo
(
receiveID varchar(20) primary key,
userID varchar2(20),
receiptName varchar2(10),
province varchar2(10),
city varchar2(10),
district varchar2(10),
street varchar2(10),
detailAdress varchar2(100),
receiptPhone char(11));


create table DBAccount(
  account varchar2(20) primary key ,
  userID varchar2(15) ,
  password varchar2(20),
  registerTime timestamp,
  lastLogin timestamp,
  lastLoginIP varchar(15));


create table DBshop
(shopID varchar2(10)primary key,
userID varchar2(10),
shopName varchar2(16),
shopIcon varchar2(10),
shopIntroduction varchar2(100),
applicationTime timestamp,
approvalTime timestamp,
favorableRate numeric(5,2),
salesValue numeric(10,2),
salesVolume int,
followVolume int)
--foreign key(userID) references DBuser);

create table DBitem (
itemID varchar2(10) not null primary key,
shopID varchar2(10),
itemName varchar2(20char),
itemScore numeric(2,1), 
itemPrice numeric(10,2),
itemFollow int,
itemIntroduction varchar2(100char)
--foreign key (shopID) references DBshop(shopID)
);



create table DBitemTag (
itemID varchar2(10),
itemTag varchar2(20char),
primary key(itemID, itemTag)
--foreign key (itemID) references DBitem(itemID)
);


create table DBitemImage (
imageID varchar2(20),
itemID varchar2(10),
itemImage blob,
primary key(imageID)
--foreign key (itemID) references DBitem(itemID)
);


create table DBorder (
orderID varchar2(10) not null primary key,
userID varchar2(10),
orderState int,
createTime date
--foreign key (userID) references DBuser(userID)
);

create table DBpayInfo(
orderID varchar2(10) not null primary key, --references DBorder(orderID),
payTime date not null,
paymoney numeric(10,2) not null
);

create table DBrefundInfo(
orderID varchar2(10) not null,-- references DBorder(orderID),
refundItemID varchar2(10) not null,-- references DBitem(itemID),
refundTime date not null,
refundReason varchar2(100),
primary key(orderID, refundItemID)
);

create table DBshopTag(
shopID varchar2(10) not null,-- references DBshop(shopID),
shopTag varchar2(20char) not null,
primary key(shopID, shopTag)
);

create table DBshopItem(
shopID varchar2(10) not null,-- references DBshop(shopID),
shopItem varchar2(10) not null,-- references DBitem(itemID),
primary key(shopID, shopItem)
);

create table DBuserInteraction(
senderID varchar2(10) not null,-- references DBuser(userID),
receiverID varchar2(10) not null,-- references DBuser(userID),
messageContent varchar2(100 char) not null,
messageTime date not null,
primary key(senderID, receiverID, messageTime)
);



create table DBitemEvaluation
(userID varchar2(10) not null,-- references DBuser(userID),
itemID varchar2(10) not null,-- references DBitem(itemID),
evaluationContent varchar2(100 char) not null,
evaluationTime date not null,
Primary key (userID, itemID)
);

Create table DBshoppingCart
(userID varchar2(10) not null,-- references DBuser(userID),
itemID varchar2(10) not null,-- references DBitem(itemID),
itemAmount int not null,
Primary key (userID, itemID)
);

Create table DBitemCollection
(userID varchar2(10) not null,-- references DBuser(userID),
itemID varchar2(10) not null,-- references DBitem(itemID),
Primary key(userID, itemID)
);

Create table DBitemAccusation
(userID varchar2(10) not null,-- references DBuser(userID),
itemID varchar2(10) not null,-- references DBitem(itemID),
AccusationTime date not null,
accusationReason varchar2(100 char) not null,
accusationState int not null,
Primary key(userID, itemID)
);

Create table DBorderitem
(orderID varchar2(10) not null,-- references DBorder(orderID),
itemID varchar2(10) not null,-- references DBitem(itemID),
itemAmount int not null,
Primary key(orderID, itemID)
);

Create table DBshopAccusation
(userID varchar2(10) not null,-- references DBuser(userID),
shopID varchar2(10) not null,-- references DBshop(shopID),
accusationReason varchar2(100 char),
accusationTime date not null,
accusationState int not null,
Primary key(userID, shopID)
);

Create table DBshopFollow
(userID varchar2(10) not null,-- references DBuser(userID),
shopID varchar2(10) not null,-- references DBshop(shopID),
Primary key(userID, shopID)
);
