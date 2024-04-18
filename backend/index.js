const express = require('express');
const { mongoDB } = require('./config/mongodb.js');
const Product = require('./models/product.model.js');
const productRoute = require('./routes/product.route.js');
const app = express();

// middleware
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
mongoDB();

//routes
app.use('/api/products', productRoute);

app.listen(3000, () => {
  console.log('server running');
});

// visit default page
app.get('/', (req, res) => {
  res.send('Hello from Node');
});
