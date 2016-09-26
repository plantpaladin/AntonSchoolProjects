#include "Bullets.h"
static const int RADIUS = 2;
static const int DAMAGE = 1;
static const int SPEED = 3;
static VGCImage bulletImage;
Bullets::Bullets(const bool &isenemy,const VGCVector &position, const VGCVector &direction) :
Entity(),
mIsAlive(true),
mIsEnemy(isenemy),
mPosition(position),
layer(FOREGROUND),
mDirection(direction){}
bool Bullets::isAlive(){
return mIsAlive;
}
bool Bullets::isEnemy(){
return mIsEnemy;}
VGCVector Bullets::getPosition(){
return mPosition;
}
int Bullets::getRadius(){
return RADIUS;
}
int Bullets::getDamage(){
return DAMAGE;
}
void Bullets::tick(EntityVector &entities){
move();
}
int Bullets::collide(Entity *entity, EntityVector &entities){
if(1 < entity->getDamage()){//inga explosioner eller andra skott
mIsAlive = false;}
return 0;
}
void Bullets::render(Layer layer){
if(layer == BACKGROUND){
const VGCVector frameIndex(0, 0);
const VGCAdjustment adjustment(0.5, 0.5);
VGCDisplay::renderImage(bulletImage, frameIndex, mPosition,
adjustment);
}
}
void Bullets::initialize(){
static const std::string filename = "Red_And_Green_Shots.png";
static const int X_FRAME_COUNT = 2;
static const int Y_FRAME_COUNT = 1;
bulletImage = VGCDisplay::openImage(filename, X_FRAME_COUNT,
Y_FRAME_COUNT);
}
void Bullets::finalize(){
VGCDisplay::closeImage(bulletImage);
}
void Bullets::move(){
mPosition += SPEED * mDirection;
}
