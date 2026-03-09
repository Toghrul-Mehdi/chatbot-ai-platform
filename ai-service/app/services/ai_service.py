from groq import Groq
from dotenv import load_dotenv
import os

load_dotenv()

client = Groq(api_key=os.getenv("GROQ_API_KEY"))

def get_ai_response(message: str, history: list = []) -> str:
    messages = [
        {"role": "system", "content": "Sən Azərbaycan dilində danışan köməkçi chatbotsan. İstifadəçinin adını və məlumatlarını dəqiq yadda saxla."}
    ]
    messages += history
    messages.append({"role": "user", "content": message})

    response = client.chat.completions.create(
        model="llama-3.3-70b-versatile",
        messages=messages
    )
    return response.choices[0].message.content