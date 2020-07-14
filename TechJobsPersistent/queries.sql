--Part 1
/* 
	Id			int
	Name		longtext
	EmployerId	int
*/

--Part 2
/*

	select * from employers where location = 'St. Louis City';

*/


--Part 3
/*
	select distinct Name, Description
	from skills
	join jobskills on jobskills.SkillId = skills.id	
	order by Name;
*/
