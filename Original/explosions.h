#ifndef EXPLOSION
#define EXPLOSION
#include "entity.h"
class Explosions :public Entity{public:
typedef std::vector<Entity*> EntityVector;
Explosions(const VGCVector &position);
bool isAlive();
bool isEnemy();
VGCVector getPosition();
int getRadius();
int getDamage();
void tick(EntityVector &entities);
int collide(Entity *entity, EntityVector &entities);
void render(Layer layer);
static void finalize();
static void initialize();
private:
VGCVector mPosition;
bool misalive;
bool misenemy;
Layer layer;};
#endif
