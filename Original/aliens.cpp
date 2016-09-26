#include "aliens.h"
#include "Bullets.h"
#include "Explosions.h"
using namespace std;
static const int RADIUS = 16;
static const int DAMAGE = 10;
static const int SCORE = 100;
static const int SPEED = 1;
static int reload_time = 40;
static VGCImage invaderImage;
//här defieneras alla variabler som bara används i det här filens funktioner
/* Invader */
Aliens::Aliens(const VGCVector &position, const VGCVector &direction) :
Entity(),
mIsAlive(true),
mIsEnemy(true),
layer(FOREGROUND),
mPosition(position),
mDirection(direction){
}//defineras alla variabler som kommer från entiey och används i funktioner som ärvs av entity
bool Aliens::isAlive(){
return mIsAlive;
}
bool Aliens::isEnemy(){
return mIsEnemy;
}
VGCVector Aliens::getPosition(){
return mPosition;
}
int Aliens::getRadius(){
return RADIUS;
}
int Aliens::getDamage(){
return DAMAGE;
}//funktioner för att få reda på variabler, också från entity
void Aliens::tick(EntityVector &entities){//funktion för vad som ska hända varje frame,röra sig
och skjuta,också från entity
move();fire(entities);}
int Aliens::collide(Entity *entity, EntityVector &entities){
if(0 < entity->getDamage()){
mIsAlive = false;
entities.push_back(new Explosions(mPosition));
return SCORE;
}//vad som ska hända ifall man kolliderar, ifall man krockar med ett skott eller skeppet ska man
förstöras och ge score
else{
return 0;
}
}
void Aliens::render(Layer layer){
if(layer == BACKGROUND){
const VGCVector frameIndex(0, 0);
const VGCAdjustment adjustment(0.5, 0.5);
VGCDisplay::renderImage(invaderImage, frameIndex, mPosition,
adjustment);
}
}
void Aliens::initialize(){
const string filename = "Green_ALien.png";
const int X_FRAME_COUNT = 1;
const int Y_FRAME_COUNT = 1;
invaderImage = VGCDisplay::openImage(filename, X_FRAME_COUNT,
Y_FRAME_COUNT);
}
void Aliens::finalize(){
VGCDisplay::closeImage(invaderImage);
}
void Aliens::move(){
mPosition += SPEED * mDirection;
int x = mPosition.getX();
int y = mPosition.getY();
const int MIN_X = RADIUS;
const int MAX_X = VGCDisplay::getWidth() - RADIUS;
if(x < MIN_X){
x = MIN_X;
mDirection.setX(-mDirection.getX());
}
if(MAX_X < x){
x = MAX_X;
mDirection.setX(-mDirection.getX());
}
mPosition.setX(x);
mPosition.setY(y);
}
void Aliens::fire(EntityVector &entities){
if (reload_time<=0){
const VGCVector direction(0, 1);
entities.push_back(new Bullets(1, mPosition, direction));
reload_time=40;}
else
{reload_time--;}
}
