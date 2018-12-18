#pragma once
#ifndef ____ASPECT_H____
#define ____ASPECT_H____

template <class T>
class BaseAspect
{
	T* m_pT;

protected:
	BaseAspect(T* pT)
		: m_pT(pT)
	{
	}

	virtual ~BaseAspect()
	{
	}

public:

	T* operator->()
	{
		return m_pT;
	}
};


template <class T>
class LogAspect : public BaseAspect<T>
{
	const char* m_name;
public:
	LogAspect(T* pT, const char* name = "")
		: BaseAspect<T>(pT)
		, m_name(name)
	{
		std::cout << "LogAspect: " << m_name << " begin" << std::endl;
	}

	~LogAspect()
	{
		std::cout << "LogAspect: " << m_name << " end" << std::endl;
	}
};


template <class T>
class LockAspect : public BaseAspect<T>
{
public:
	LockAspect(T* pT)
		: BaseAspect<T>(pT)
	{
		std::cout << "LockAspect: lock" << std::endl;
	}

	~LockAspect()
	{
		std::cout << "LockAspect: unlock" << std::endl;
	}
};


#endif//____ASPECT_H____
