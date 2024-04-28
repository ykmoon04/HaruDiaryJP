# Use the official Python image as the base image
FROM python:3.8-slim

# Set the working directory inside the container
WORKDIR /app

# Copy the requirements file into the container
COPY requirements.txt .

# Install the dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Copy the Flask app files into the container
COPY app.py analyze.py ./

# Expose the port on which your Flask app will run
EXPOSE 5000

# Command to run your Flask app
CMD ["python", "app.py"]
