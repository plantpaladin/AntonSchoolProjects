#pragma once
#include "Object.h"
class Player : public Object
{
public:
	Player(float x,float y, float raidus,sf::Sprite * sprite);
	virtual void update(float deltaTime);
	bool isShooting();
	~Player();
private:
	float m_timeSincelastShot;
};

