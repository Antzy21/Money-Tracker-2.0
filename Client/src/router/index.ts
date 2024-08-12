import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Categories from '../views/CategoriesView.vue'
import ToDoView from '../views/ToDoView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/categories',
      name: 'Categories',
      component: Categories
    },
    {
      path: '/todo',
      name: 'ToDo',
      component: ToDoView
    }
  ]
})

export default router
