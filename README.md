# Banished
ProtoType Project I finally have the time to build



How it works:

BlackBoard Manager-
	This class manages "factions" and objects belonging to factions.
	In order for any object to "exist" it must belong to a Blackboard managing a faction,
	this includes Trees,Rocks, etc.
	
Collision Manager-
	This class manages detecting collisions within the world.
	It creates a "play area" specified by Dimension, and divides it in the X & Y by
	the divisions requested in DivCount
	
	For now each "Division" is split into a "Sector" class, the sector classes are each responsible for
	checking collisions within their classes with a specified actor input.
	In order for a collision to be tested the actors sector gets calculated, the corresponding sector
	then performs square box overlap detection on all objects within itself if an overlap occurs it reports it
	as true, otherwise continues till all values are checked.
	
	
When an Entity Static or Dynamic gets created it must:
	1)Be Added to the BlackBoard Manager under a Faction
	2)Be Added to a Sector via Collision Manager
		-If it's a dynamic object (moving object), this part is automatic as units constantly update
			their sector information thus this can be ignored
		-If it's a static object (stationary object), it must be manually invoked via the place
		building function call. If its position is invalid due to overlap then the object does not get placed.
			Note: Because of this check, it's more logical to test adding buildings here before BlackBoard Manager.
			
			
			
Sector -
	A class functioning as a "container" or "bin" of objects for collision handling
	
BlackBoard -
	A Faction container containing a list of all current LIVE objects regardless their positioning
	

When a unit dies it must therefore get "removed" from both the blackboard and sector screen.


Entities Capable of Collision:
	Static Objects - Buildings, Trees, rocks etc.
	Dynamic Objects - Units/Actors, and projectiles
	
	Resources- These objects should get their function handled before anything else:
		A) They are not "visible", they simply exist in Inventory systems
		B) They ARE visible
			B.1) Units can walk over them "No need to add to sectors"
			B.2) Units can't walk over them (Add to sectors)




Process Assignment:
	To assign a worker to a building click the building and press +

	The + calls an event in the button to send a message to GController
	GController then passes the message to BlackBoardManager for the Player Faction
	Blackboard then links the building with an available unit.
	
	The buildings job type gets created in building, and sent to the actor.
	The building appends the actor to its assigned workers list for upkeep 
		(useful incase the building gets destroyed).
	The actor sets his job to what the building stated.
	
	When an Actor decides an action to perform, he consults his "mood" and his "job"
		These dictate the behaviour of the Actor in each scenario.
	
	-Collection Jobs require information in regards to items in proximity of the assigned building
	-
	
	
	
	
	
	
	


	
	
	