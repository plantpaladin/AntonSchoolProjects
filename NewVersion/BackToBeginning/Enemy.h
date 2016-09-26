#pragma once
#include "Object.h"
#include "SFML\Graphics\Texture.hpp"
class Enemy : public Object
{
public:
	Enemy(int x, int y, int radius, std::string spriteName);
	virtual void update()
	~Enemy();
private:
	sf::Texture m_texture;

};

