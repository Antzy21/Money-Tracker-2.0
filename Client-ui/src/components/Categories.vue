<template>
  <div class="container">
    <h1>Categories</h1>
    <Tree :nodes="categoryTree" />
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import Tree from 'vuejs-tree'
//https://openbase.com/js/vuejs-tree/documentation

export default {
  name: 'Categories',
  data() {
    return {
      treeDisplayData: [
        {
          text: 'Root 1',
          state: { checked: false, selected: false, expanded: false },
          nodes: [
            {
              text: 'Child 1',
              state: { checked: false, selected: false, expanded: false }
            }
          ]
        },
        {
          text: 'Root 2',
          state: { checked: false, selected: false, expanded: false }
        }
      ]
    }
  },
  components: {
    'Tree': Tree
  },
  computed: {
    ...mapState(['categories']),
    categoryTree() {
      this.categories.forEach(category => {
        this.convertCategoryView(category)
      });
      return this.categories
    },
  },
  methods: {
    ...mapActions(['addCategory']),
    convertCategoryView(category) {
      category.state = {
        checked: false,
        expanded: false,
        selected: false,
      }
      category.text = category.name
      return category
    },
  }
}
</script>