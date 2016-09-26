#ifndef BULLETS
#define BULLETS
#include "entity.h"
class Bullets : public Entity{
public:
Bullets(const bool &misEnemy,const VGCVector &position, const VGCVector &direction);
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
bool mIsEnemy;
bool mIsAlive;
Layer layer;
VGCVector mPosition;
VGCVector mDirection;
};
#endif
