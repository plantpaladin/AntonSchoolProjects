#ifndef SHIP
#define SHIP
#include "Entity.h"
class Ship : public Entity{
public:
Ship(const VGCVector &position);
typedef std::vector<Entity*> EntityVector;
Ship();
virtual bool isAlive();
virtual bool isEnemy();
virtual VGCVector getPosition();
virtual int getRadius();
virtual int getDamage();
virtual void tick(EntityVector &entities);
virtual int collide(Entity *entity, EntityVector &entities);
virtual void render(Layer layer) ;
int getLife();
void fire(EntityVector &entities);
static void initialize();
static void finalize();
private:
bool mIsEnemy;
bool mIsAlive;
int life;
VGCTimer mReloadTimer;
VGCVector mPosition;
VGCVector mDirection;
};
#endif
