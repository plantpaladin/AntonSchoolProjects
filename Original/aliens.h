#ifndef ALIENS
#define ALIENS
#include "Entity.h"
/* Invader */
class Aliens : public Entity{
public:
Aliens(const VGCVector &position, const VGCVector &direction);
bool isAlive();
bool isEnemy();
VGCVector getPosition();
int getRadius();
int getDamage();
void tick(EntityVector &entities);
int collide(Entity *entity, EntityVector &entities);
void render(Layer layer);
static void initialize();
static void finalize();
private:
void move();
void fire(EntityVector &entities);
bool mIsAlive;
bool mIsEnemy;
VGCVector mPosition;
VGCVector mDirection;
Layer layer;
};
#endif
