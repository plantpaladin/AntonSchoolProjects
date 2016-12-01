#pragma once
#include <math.h>
#include "SFML\Graphics\Sprite.hpp"
class Object
{
protected:

	float m_x, m_y, m_radius,m_speed;
	sf::Sprite * m_sprite;
public:
	//not defined here as noone should try to instanciate object directly
	virtual void update(float deltaTime)=0;//unique for all objects, primarly movement
	
	virtual bool collide(sf::Sprite * sprite)//same for all objects
	{
		if (m_sprite->getGlobalBounds().intersects(sprite->getGlobalBounds()))
		{
			return true;
		}
		return false;
	}
	virtual sf::Sprite * getSprite()
	{
		m_sprite->setPosition(m_x, m_y);
		return m_sprite;
	}

	virtual sf::Vector2f getPosition()
	{
		return sf::Vector2f(m_x, m_y);
	}
};

