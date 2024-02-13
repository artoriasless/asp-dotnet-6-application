-- dak_site_demo.test definition

CREATE TABLE `test` (
  `Id` int NOT NULL AUTO_INCREMENT COMMENT '主键 ID',
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '名称',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;