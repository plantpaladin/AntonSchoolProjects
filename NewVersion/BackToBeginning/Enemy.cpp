#include "Enemy.h"
#include <string>
#include <iostream>


Enemy::Enemy(int x,int y, int radius,std::string spriteName)
{
	m_x = x;
	m_y = y;
	m_radius = radius;
	if (!m_texture.loadFromFile(spriteName, sf::IntRect(10, 10, 64, 64)))
	{
		std::cout << "Failure \n";
	}
}

void Enemy::update()
{
}
Enemy::~Enemy()
{
}
