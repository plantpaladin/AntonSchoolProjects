#pragma once
class Shot
{
public:
	Shot();
	void update();
	bool isColliding(int x1,int x2,int y1,int y2);
	~Shot();
};

