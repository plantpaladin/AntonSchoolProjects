#include "ship.h"
#include "Bullets.h"
using namespace std;
static const int RADIUS = 16;
static const int DAMAGE = 10;
static const int SCORE = 100;
static const int SPEED = 2;
static int LIFE=20;
static int RELOAD_SPEED = 0;
static VGCImage shipimage;
/* Invader */
Ship::Ship(const VGCVector &position) :
Entity(),
mIsAlive(true),
mIsEnemy(false),
mPosition(position){}
bool Ship::isAlive(){
return mIsAlive;
}
int Ship::getLife(){
return LIFE;
};
bool Ship::isEnemy(){
return mIsEnemy;
}
VGCVector Ship::getPosition(){
return mPosition;
}
int Ship::getRadius(){
return RADIUS;
}
int Ship::getDamage(){
return DAMAGE;
}
void Ship::tick(EntityVector &entities){
if
((VGCKeyboard::isPressed(VGCKey::SPACE_KEY))
&&(RELOAD_SPEED<=0))
{fire(entities);}
else{RELOAD_SPEED--;}
if ((VGCKeyboard::isPressed(VGCKey::ARROW_LEFT_KEY)==1)
&&(VGCKeyboard::isPressed(VGCKey::ARROW_RIGHT_KEY)==0)
&&(mPosition.getX()>16))
{
mPosition.setX(mPosition.getX()-SPEED);
}
if ((VGCKeyboard::isPressed(VGCKey::ARROW_LEFT_KEY)==0)
&&(VGCKeyboard::isPressed(VGCKey::ARROW_RIGHT_KEY)==1)
&&(mPosition.getX()<624))
{
mPosition.setX(mPosition.getX()+SPEED);
}
if ((VGCKeyboard::isPressed(VGCKey::ARROW_UP_KEY)==1)
&&(VGCKeyboard::isPressed(VGCKey::ARROW_DOWN_KEY)==0)
&&(mPosition.getY()>16))
{
mPosition.setY(mPosition.getY()-SPEED);
}
if ((VGCKeyboard::isPressed(VGCKey::ARROW_UP_KEY)==0)
&&(VGCKeyboard::isPressed(VGCKey::ARROW_DOWN_KEY)==1)
&&(mPosition.getY()<470))
{
mPosition.setY(mPosition.getY()+SPEED);
}
}
int Ship::collide(Entity *entity, EntityVector &entities){
LIFE-=entity->getDamage();
return 0;
}
void Ship::fire(EntityVector &entities)
{VGCVector direction(1,-1);
VGCVector direction2(-1,-1);
VGCVector direction3(0,-1);
RELOAD_SPEED=40;entities.push_back(new Bullets(false,mPosition,direction));
entities.push_back(new Bullets(false,mPosition,direction2));
entities.push_back(new Bullets(false,mPosition,direction3));
};
void Ship::render(Layer layer){
if(layer == BACKGROUND){
const VGCVector frameIndex(0, 0);
const VGCAdjustment adjustment(0.5, 0.5);
VGCDisplay::renderImage(shipimage, frameIndex, mPosition,
adjustment);
}
}
void Ship::initialize(){
const string filename="Red_Guy.png";
const int X_FRAME_COUNT = 1;
const int Y_FRAME_COUNT = 1;
shipimage = VGCDisplay::openImage(filename, X_FRAME_COUNT,
Y_FRAME_COUNT);
}
void Ship::finalize(){
VGCDisplay::closeImage(shipimage);
}

