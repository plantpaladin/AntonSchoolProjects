#ifndef INVADERS
#define INVADERS
#include "Ship.h"
#include "aliens.h"
#include <string>
#include "Entity.h"
/* Invaders */
class Invaders{
public:
typedef std::vector<Entity*> EntityVector;
void run();
Invaders();
private:
void spawnInvader();
void tick();
void detectCollisions();
void renderBackground();
void renderForeground();
void renderLife();
void renderScore();
void killDeadEntities();
void killEdgeEntities();
static bool isOverlap(Entity *entity0, Entity *entity1);
Ship *mShip;
Aliens *mAlien;
EntityVector mEntities;
int mScore;
VGCFont mFont;
};
#endif
