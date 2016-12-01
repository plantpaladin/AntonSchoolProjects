#pragma once
#include "Object.h"
#include "SFML\Graphics\Texture.hpp"

class Shot : public Object
{
public:
	Shot(float x, float y, float radius, float speed, sf::Sprite *  sprite);
	void newPosition(sf::Vector2f newPosition);
	virtual void update(float deltaTime);
	~Shot();
};

