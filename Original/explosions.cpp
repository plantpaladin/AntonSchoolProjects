#include "explosions.h"
#include <string>
static const int RADIUS = 2;
static VGCImage explosionimage;
static int LIFE_TIME = 120;
Explosions::Explosions(const VGCVector &position) :
Entity(),
misalive(true),
misenemy(true),
layer(BACKGROUND),
mPosition(position)
{}
bool Explosions::isAlive(){return misalive;}
bool Explosions::isEnemy(){return misenemy;}
VGCVector Explosions::getPosition(){return mPosition;}
int Explosions::getRadius(){
return 0;}
int Explosions::getDamage(){
return 0;}
void Explosions::tick(EntityVector &entities){
if (LIFE_TIME<=0){misalive=false; LIFE_TIME=120;}else{LIFE_TIME--;}
}
int Explosions::collide(Entity *entity, EntityVector &entities){return 0;}
void Explosions::render(Layer layer){
if(layer == BACKGROUND){
const VGCVector frameIndex(0, 0);
const VGCAdjustment adjustment(0.5, 0.5);
VGCDisplay::renderImage(explosionimage, frameIndex, mPosition,adjustment);
}
}
void Explosions::initialize(){
static const std::string filename = "explosions.png";
static const int X_FRAME_COUNT = 1;
static const int Y_FRAME_COUNT = 1;
explosionimage = VGCDisplay::openImage(filename, X_FRAME_COUNT,
Y_FRAME_COUNT);
}
void Explosions::finalize(){
VGCDisplay::closeImage(explosionimage);
}
