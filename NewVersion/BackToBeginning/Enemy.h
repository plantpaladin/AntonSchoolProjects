#pragma once
#include "Object.h"
#include "SFML\Graphics\Texture.hpp"
class Enemy : public Object
{
public:
	Enemy(float x, float y, float radius,float speed, sf::Sprite * sprite);
	void newPosition(int x);//gives a new position to the object(used when entering from object pool)

	virtual void update(float deltaTime);
	virtual bool canShoot();
	~Enemy();
private:
	float m_timeSinceLastShot;
	int movingRight;
};

