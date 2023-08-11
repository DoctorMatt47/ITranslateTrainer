<script lang="ts">
  export let className = "";
</script>

<button class="btn variant-ghost-surface {className}" on:click type="button">
  <slot />
</button>
