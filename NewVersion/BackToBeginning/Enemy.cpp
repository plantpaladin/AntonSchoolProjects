#include "Enemy.h"
#include <string>
#include <iostream>
static float enemyFiringSpeed = 2.0;
static float maxX = 512;//the width of the screen minus the radius of the ship * 2

Enemy::Enemy(float x,float y, float radius,float speed,sf::Sprite * sprite)
{
	m_x = x;
	m_y = y;
	m_radius = radius;
	m_sprite = sprite;
	m_speed = speed;
	movingRight = 1;
	m_timeSinceLastShot = 0;
}

void Enemy::newPosition(int x)
{
	movingRight = 1;
	m_y = 0;
	m_x = x;
}

void Enemy::update(float deltaTime)//delta time in seconds
{
	m_timeSinceLastShot += deltaTime;
	
	m_y += m_speed*deltaTime;
	float nextX = m_x + deltaTime*m_speed*movingRight;
	
	if (movingRight ==true)
	{
		if (nextX > maxX)
		{
			m_x = maxX;
			movingRight = -1;
		}
		else
		{
			m_x = nextX;
		}
	}
	else
	{
		if (nextX < m_radius)
		{
			m_x = m_radius;
			movingRight = 1;
		}
		else
		{
			m_x = nextX;
		}
	}
}

bool Enemy::canShoot()
{
	if (m_timeSinceLastShot >= enemyFiringSpeed)
	{
		m_timeSinceLastShot -= enemyFiringSpeed;
		return true;
	}
	return false;
}
Enemy::~Enemy()
{
	delete m_sprite;
}
